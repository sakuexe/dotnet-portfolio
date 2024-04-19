using System.Text.Json;
using fullstack_portfolio.Data;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Route("Dashboard/[controller]")]
public class GenericController<T> : Controller where T : IMongoModel
{
    private class Error {
        public string Field { get; set; } = string.Empty;
        public string[] Messages { get; set; } = Array.Empty<string>();
    }

    private Error[] GetErrors()
    {
        return ModelState
            .Where(s => s.Value?.Errors.Count() > 0)
            .Select(s => new Error
            {
                Field = s.Key,
                Messages = s.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            }).ToArray();
    }

    public IActionResult Index()
    {
        ViewBag.Collection = typeof(T).Name;
        List<T> models = MongoContext.GetAll<T>();
        return View("Views/Dashboard/Collection.cshtml", models);
    }

    [HttpGet("new")]
    public IActionResult New()
    {
        return View("Views/Dashboard/Edit.cshtml", Activator.CreateInstance<T>());
    }

    [HttpGet("{id}")]
    public IActionResult Edit(string id)
    {
        T? model = MongoContext.Get<T>(id);
        if (model == null)
            return NotFound();
        return View("Views/Dashboard/Edit.cshtml", model);
    }

    // generic controller for saving changes to any item specified in the program.cs
    [HttpPost("{id}/save")]
    public IActionResult Save(T model)
    {
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        MongoContext.Save(model);
        return Ok();
    }
}

