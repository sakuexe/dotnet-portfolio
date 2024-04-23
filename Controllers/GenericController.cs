using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using fullstack_portfolio.Data;
using fullstack_portfolio.Utils;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Route("Dashboard/[controller]")]
public class GenericController<T> : Controller where T : IMongoModel, new()
{
    public virtual int ImageWidth { get; set; } = 0;
    public virtual int ImageHeight { get; set; } = 0;
    public virtual string? ImageProperty { get; set; }

    // I tried using a struct, but when I do, the result is always empty
    protected class Error {
        public string Field { get; set; } = string.Empty;
        public string[] Messages { get; set; } = Array.Empty<string>();
    }

    protected Error[] GetErrors()
    {
        // get all errors and group them by input field, 
        // for ease of use in the frontend. like this:
        // { "Title": ["Title is required", "Title must be at least 3 characters long"] }
        return ModelState
            .Where(s => s.Value?.Errors.Count() > 0)
            .Select(s => new Error
            {
                Field = s.Key,
                Messages = s.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            }).ToArray();
    }

    [HttpGet]
    public virtual IActionResult Index()
    {
        // List all the items in a collection
        ViewBag.Collection = typeof(T).Name;
        ViewBag.Title = $"{typeof(T).Name}s";
        List<T> models = MongoContext.GetAll<T>();
        if (models.Count() < 1)
            return View("Views/Dashboard/NoResults.cshtml", new T());
        return View("Views/Dashboard/Collection.cshtml", models);
    }

    [HttpGet("new")]
    public virtual IActionResult New()
    {
        ViewBag.Title = $"New {typeof(T).Name}";
        // create a new item, initialize it with a new instance
        return View("Views/Dashboard/Edit.cshtml", Activator.CreateInstance<T>());
    }

    [HttpGet("{id}")]
    public virtual IActionResult Edit(string id)
    {
        // get the edit page for an item and fill it with the data
        T? model = MongoContext.Get<T>(id);
        if (model == null)
            return NotFound();
        ViewBag.Title = model._id.ToString().Substring(0, 8) + "...";
        return View("Views/Dashboard/Edit.cshtml", model);
    }

    [HttpPost("{id}/Save")]
    public virtual async Task<IActionResult> Save(T model, IFormFile? file = null)
    {
        // save the item to the database
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        if (file == null)
        {
            // save the expertise
            await MongoContext.Save(model);
            return Ok();
        }

        // check that the ImageProperty is set if the file is sent
        if (string.IsNullOrEmpty(ImageProperty))
        {
            Error[] errors = { new Error { Field = "Icon", Messages = new string[] { "ImageProperty variable is not set inside controller" } } };
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        // save to disk if the icon is given
        string? savedPath = await FileUtils.SaveFile(file);
        if (savedPath == null)
        {
            Error[] errors = { new Error { Field = "Icon", Messages = new string[] { "File could not be saved" } } };
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        // resize the image (svgs not supported)
        string filetype = Path.GetExtension(savedPath);
        if (filetype == ".svg" || !FileUtils.ResizeImage(savedPath, ImageWidth, ImageHeight))
            Debug.WriteLine("Couldn't resize the image");

        T? currentModelData = MongoContext.Get<T>(model._id.ToString());
        if (currentModelData != null)
        {
            // fetch all the expertise items that have the same icon
            string? currentIcon = currentModelData.GetType().GetProperty(ImageProperty)?.GetValue(currentModelData) as string;
            if (currentIcon == null)
                return BadRequest("Icon property is not set correctly in the model");

            List<T> records = await MongoContext.Filter<T>(ImageProperty!, currentIcon);
            // do not remove the file if it's being used by another item
            if (records.Count() < 1 && currentIcon != savedPath)
                FileUtils.DeleteFile(currentIcon ?? "");
        }

        // get the model property that has datatype of ImageUrl
        PropertyInfo? prop = model.GetType().GetProperty(ImageProperty ?? "");
        if (prop == null)
        {
            Error[] errors = { new Error { 
                Field = ImageProperty ?? "",
                Messages = new string[] { 
                    $"Property variable ({ImageProperty}) is set incorrectly inside controller" 
                }} 
            };
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        // finally, if all is well, save the path to the model and save the model
        prop.SetValue(model, savedPath);
        await MongoContext.Save(model);
        return Ok();
    }
}

