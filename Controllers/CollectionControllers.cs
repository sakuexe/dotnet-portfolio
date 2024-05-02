using fullstack_portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio.Controllers;

public class BioController : GenericController<Bio>
{
    public override async Task<IActionResult> Save(Bio model, IFormFile? file = null)
    {
        model.UpdatedAt = DateTime.Now;
        return await base.Save(model, file);
    }
}

// implement the controllers for the collections
public class ExpertiseController : GenericController<Expertise>
{
    // when the controller has a property with an image
    // these two will have to be overridden, for the image to be saved
    public override int ImageWidth { get; set; } = 128;
    public override string ImageProperty { get; set; } = "Icon";
}

public class SkillController : GenericController<Skill> { }

public class ProjectController : GenericController<Project>
{
    public override string EditView { get; set; } = "Views/Dashboard/Edit/Project.cshtml";
    public override string ImageProperty { get; set; } = "ImageUrl";
    public override int ImageWidth { get; set; } = 1500;
    public override int ThumbnailWidth { get; set; } = 512;
}

public class ExperienceController : GenericController<Experience>
{
    public override string ImageProperty { get; set; } = "ImageUrl";
    public override int ImageWidth { get; set; } = 512;
    public override string EditView { get; set; } = "Views/Dashboard/Edit/Experience.cshtml";
}

public class ContactInfoController : GenericController<ContactInfo>
{
    public override string EditView { get; set; } = "Views/Dashboard/Edit/ContactInfo.cshtml";
}

public class UserController : GenericController<User>
{
    [HttpPost("{id}/Save")]
    public override async Task<IActionResult> Save(User model, IFormFile? file = null)
    {
        model.Password = PasswordHasher.HashPassword(model.Password);
        return await base.Save(model, file);
    }
}
