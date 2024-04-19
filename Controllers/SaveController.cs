using System.Text.Json;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

[Route("Dashboard/")]
public class SaveController : Controller
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

    // Save changes to expertise items
    [HttpPost("expertise/{id}/save")]
    public IActionResult SaveExpertise(Expertise expertise)
    {
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            // send json response with errors, use status code 400
            Console.WriteLine(JsonSerializer.Serialize(errors));
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        MongoContext.Save(expertise);
        return Ok();
    }

    // Save changes to expertise items
    [HttpPost("skill/{id}/save")]
    public IActionResult SaveSkill(Skill skill)
    {
        if (!ModelState.IsValid)
        {
            var errors = GetErrors();
            // send json response with errors, use status code 400
            Console.WriteLine(JsonSerializer.Serialize(errors));
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        MongoContext.Save(skill);
        return Ok();
    }
}
