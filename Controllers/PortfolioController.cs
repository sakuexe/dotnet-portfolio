using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

public class PortfolioController : Controller
{
    public async Task<IActionResult> Index()
    {
        List<Project> projects = await MongoContext.GetAll<Project>();
        if (projects.Count == 0)
            return NotFound();

        Project newestProject = projects.Last();
        return RedirectToAction(nameof(Details), new { id = newestProject._id });
    }

    [Route("[controller]/{id}")]
    public IActionResult Details(string id)
    {
        Project? project = MongoContext.Get<Project>(id);
        if (project == null)
            return NotFound();

        return View(project);
    }
}
