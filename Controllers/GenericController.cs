using System.Reflection;
using System.Text.Json;
using fullstack_portfolio.Data;
using fullstack_portfolio.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Authorize]
[Route("Dashboard/[controller]")]
public class GenericController<T> : Controller where T : IMongoModel, new()
{
    public virtual int ImageWidth { get; set; } = 0;
    public virtual int ImageHeight { get; set; } = 0;
    public virtual int ThumbnailWidth { get; set; } = 0;
    public virtual string ImageProperty { get; set; } = "ImageUrl";
    public virtual string ThumbnailProperty { get; set; } = "ThumbnailUrl";
    public virtual string EditView { get; set; } = "Views/Dashboard/Edit/Generic.cshtml";

    // I tried using a struct, but when I do, the result is always empty
    protected class Error
    {
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
    public async virtual Task<IActionResult> Index()
    {
        // List all the items in a collection
        ViewBag.Collection = typeof(T).Name;
        ViewBag.Title = $"{typeof(T).Name}s";
        List<T> models = await MongoContext.GetAll<T>();
        if (models.Count() < 1)
            return View("Views/Dashboard/NoResults.cshtml", new T());
        return View("Views/Dashboard/Collection.cshtml", models.ToArray());
    }

    [HttpGet("new")]
    public virtual IActionResult New()
    {
        ViewBag.Title = $"New {typeof(T).Name}";
        ViewBag.Collection = typeof(T).Name;
        // create a new item, initialize it with a new instance
        return View(EditView, new T());
    }

    [HttpGet("{id}")]
    public virtual IActionResult Edit(string id)
    {
        // get the edit page for an item and fill it with the data
        T? model = MongoContext.Get<T>(id);
        if (model == null)
            return NotFound();
        ViewBag.Title = model._id.ToString().Substring(0, 8) + "...";
        ViewBag.Collection = typeof(T).Name;
        return View(EditView, model);
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

        // save to disk if the icon is given
        string? savedPath;
        try {
            Console.WriteLine($"Saving file {file.FileName}");
            savedPath = await ImageManipulator.SaveImage(file, ImageWidth);
        } catch (Exception e) {
            Console.WriteLine($"Error saving file: {e.Message}");
            Error[] errors = { new Error { Field = ImageProperty, Messages = new string[] { e.Message } } };
            return BadRequest(JsonSerializer.Serialize(errors));
        }


        // generate a thumbnail if the width is set
        string? thumbnailPath = null;
        if (ThumbnailWidth > 0)
        {
            try {
                thumbnailPath = await ImageManipulator.GenerateThumbnail(file, ThumbnailWidth);
                if (thumbnailPath == null)
                    throw new Exception($"Error generating thumbnail: {thumbnailPath}");
            } catch (Exception e) {
                Console.WriteLine($"Error generating thumbnail: {e.Message}");
                Error[] errors = { new Error { Field = ImageProperty, Messages = new string[] { e.Message } } };
                return BadRequest(JsonSerializer.Serialize(errors));
            }
        }

        // check if the image is used by another item
        T? currentModelData = MongoContext.Get<T>(model._id.ToString());
        string? currentImage = currentModelData?.GetType().GetProperty(ImageProperty)?.GetValue(currentModelData) as string;
        if (currentImage != null)
        {
            // fetch all the expertise items that have the same icon
            List<T> records = await MongoContext.Filter<T>(ImageProperty!, currentImage);
            // do not remove the file if it's being used by another item
            if (records.Count() < 1 && currentImage != savedPath)
                FileUtils.DeleteFile(currentImage ?? "");
        }

        // get the model property that has datatype of ImageUrl
        PropertyInfo? prop = model.GetType().GetProperty(ImageProperty ?? "");
        if (prop == null)
        {
            Console.WriteLine($"No variable ({ImageProperty}) could be found in the model {model.GetType().Name}");
            Error[] errors = { new Error {
                Field = "general-error",
                Messages = new string[] {
                    $"No variable ({ImageProperty}) could be found in the model {model.GetType().Name}"
                }}
            };
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        // finally, if all is well, save the path to the model and save the model
        prop.SetValue(model, savedPath);
        // save the thumbnail path if it's set
        if (thumbnailPath is not null)
        {
            PropertyInfo? thumbnailProp = model.GetType().GetProperty(ThumbnailProperty ?? "");
            if (thumbnailProp is null)
            {
                Error[] errors = { new Error {
                    Field = "general-error",
                    Messages = new string[] {
                        $"No variable ({ThumbnailProperty}) could be found in the model {model.GetType().Name}"
                    }}
                };
                return BadRequest(JsonSerializer.Serialize(errors));
            }
            Console.WriteLine($"Setting thumbnail to {thumbnailPath}");
            thumbnailProp.SetValue(model, thumbnailPath);
        }
        await MongoContext.Save(model);
        return Ok();
    }
}

