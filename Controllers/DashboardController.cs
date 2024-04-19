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

    public IActionResult Index()
    {
        DashboardViewModel model = new();
        // order the models to be in the same order as they are in the database
        return View(model);
    }
}
