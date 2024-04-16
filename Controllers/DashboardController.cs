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
    [HttpGet("[controller]/{parameter}")]
    public ActionResult Collection(string parameter)
    {
        ViewData["Collection"] = parameter;
        var className = $"fullstack_portfolio.Models.{Capitalize(parameter)}";
        Type? type = Type.GetType(className);
        if (type == null)
        {
            // TODO: add a 404 page
            return NotFound();
        }
        // using reflection to call a generic method
        // this is extra messy, but it works
        MethodInfo method = typeof(MongoContext).GetMethod("GetAll")!;
        MethodInfo generic = method.MakeGenericMethod(type);
        var mongoCollection = generic.Invoke(new MongoContext(), new Object[] { parameter });
        return View(mongoCollection);
    }
}
