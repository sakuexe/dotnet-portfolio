using System.Text.Json;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Route("Dashboard/")]
public class EditController : Controller
{
    // Save changes to expertise items
    [HttpPost("expertise/{id}/save")]
    public IActionResult SaveExpertise(Expertise expertise)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        MongoContext.Save(expertise);
        return RedirectToAction("Index", "Dashboard");
    }
    // Save changes to expertise items
    [HttpPost("skill/{id}/save")]
    public IActionResult SaveSkill(Skill expertise)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            // send json response with errors, use status code 400
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        MongoContext.Save(expertise);
        return RedirectToAction("Index", "Dashboard");
    }
}
