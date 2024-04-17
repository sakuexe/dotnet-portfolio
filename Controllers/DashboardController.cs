using System.Reflection;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
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
        return View(model);
    }

    // dynamic route, to look for a collection by name
    [HttpGet("[controller]/{collection:alpha}")]
    public IActionResult Collection(string collection)
    {
        Console.WriteLine($"Collection: {collection}");
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

    [HttpGet("[controller]/{collection:alpha}/{id}")]
    public IActionResult Details(string collection, string id)
    {
        Console.WriteLine($"Collection: {collection}, ID: {id}");
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

    // Save changes to expertise items
    [HttpPost("[controller]/expertise/{id}/save")]
    public IActionResult Save(Expertise expertise)
    {
        Console.WriteLine("Saving expertise");
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Model is not valid");
            return View("Details", expertise);
        }

        MongoContext.Save(expertise);
        return RedirectToAction("Index");
    }
}
