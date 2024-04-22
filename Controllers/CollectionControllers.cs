using System.Diagnostics;
using System.Text.Json;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using fullstack_portfolio.Utils;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

// implement the controllers for the collections
public class ExpertiseController : GenericController<Expertise>
{
    public override async Task<IActionResult> Save(Expertise model, IFormFile? file = null)
    {
        if (!ModelState.IsValid)
            return BadRequest(JsonSerializer.Serialize(GetErrors()));

        if (file == null)
        {
            // save the expertise
            await MongoContext.Save(model);
            return Ok();
        }

        // if the icon is given
        string? savedPath = await FileUtils.SaveFile(file);
        if (savedPath == null)
        {
            Error[] errors = { new Error { Field = "Icon", Messages = new string[] { "File could not be saved" } } };
            return BadRequest(JsonSerializer.Serialize(errors));
        }

        string filetype = Path.GetExtension(savedPath).Replace(".", "");
        // resize the image to 128 px wide
        // not passing the height will maintain the aspect ratio
        if (filetype == "svg" || !FileUtils.ResizeImage(savedPath, 128))
            Debug.WriteLine("Couldn't resize the image");

        string? currentIcon = MongoContext.Get<Expertise>(model._id.ToString())?.Icon;
        // fetch all the expertise items that have the same icon
        List<Expertise> records = await MongoContext.Filter<Expertise>("icon", currentIcon ?? "");
        // do not remove the file if it's the same as the current one
        if (records.Count() < 1 && currentIcon != savedPath)
            FileUtils.DeleteFile(currentIcon ?? "");
        model.Icon = savedPath;

        // save the expertise
        await MongoContext.Save(model);
        return Ok();
    }
}

public class SkillController : GenericController<Skill> { }
