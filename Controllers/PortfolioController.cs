using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

public class PortfolioController : Controller
{
    [Route("[controller]/{id}")]
    public IActionResult Index(string id)
    {
        Console.WriteLine(id);
        Project? project = MongoContext.Get<Project>(id);
        Console.WriteLine(project?.Title);
        if (project == null)
            return NotFound();

        return View(project);
    }
}
