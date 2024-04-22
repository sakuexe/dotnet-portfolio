using System.Text.Json;
using fullstack_portfolio.Data;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Route("Dashboard/[controller]")]
public class GenericController<T> : Controller where T : IMongoModel
{
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

        await MongoContext.Save(model);
        return Ok();
    }
}

