using System.Reflection;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        return input.Substring(0, 1).ToUpper() + input.Substring(1);
    }

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[controller]/")]
    public IActionResult Index()
    {
        DashboardViewModel model = new();
        // order the models to be in the same order as they are in the database
        return View(model);
    }

    // dynamic route, to look for a collection by name
    [HttpGet("[controller]/{collection:alpha}")]
    public IActionResult Collection(string collection)
    {
        ViewData["Collection"] = collection;
        var className = $"fullstack_portfolio.Models.{Capitalize(collection)}";
        Type? type = Type.GetType(className);
        if (type == null)
            return NotFound();

        // using reflection to call a generic method
        // this is extra messy, but it works
        MethodInfo method = typeof(MongoContext).GetMethod("GetAll")!;
        MethodInfo generic = method.MakeGenericMethod(type);
        var mongoCollection = generic.Invoke(new MongoContext(), new Object[] { collection });

        return View(mongoCollection);
    }

    // dynamic route for creating a new item in the collection
    [HttpGet("[controller]/{collection:alpha}/new")]
    public IActionResult New(string collection)
    {
        var className = $"fullstack_portfolio.Models.{Capitalize(collection)}";
        Type? type = Type.GetType(className);
        if (type == null)
            return NotFound();
        var model = Activator.CreateInstance(type);
        return View("Edit", model);
    }

    // dynamic route for editing an item in the collection
    [HttpGet("[controller]/{collection:alpha}/{id}")]
    public IActionResult Edit(string collection, string id)
    {
        // the Collection gets parsed in the view from the URL path
        var className = $"fullstack_portfolio.Models.{Capitalize(collection)}";
        Type? type = Type.GetType(className);
        if (type == null)
            return NotFound();

        // using reflection to call a generic method
        MethodInfo method = typeof(MongoContext).GetMethod("Get")!;
        MethodInfo generic = method.MakeGenericMethod(type);
        var mongoItem = generic.Invoke(new MongoContext(), new Object[] { id });
        if (mongoItem == null)
            return NotFound();

        return View(mongoItem);
    }
}
