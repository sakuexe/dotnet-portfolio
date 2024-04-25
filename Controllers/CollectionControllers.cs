using fullstack_portfolio.Models;

namespace fullstack_portfolio.Controllers;

// implement the controllers for the collections
public class ExpertiseController : GenericController<Expertise>
{
    // when the controller has a property with an image
    // these two will have to be overridden, for the image to be saved
    public override int ImageWidth { get; set; } = 128;
    public override string? ImageProperty { get; set; } = "Icon";
}

public class SkillController : GenericController<Skill> { }

public class ProjectController : GenericController<Project> 
{ 
    public override string EditView { get; set; } = "Views/Dashboard/Edit/Project.cshtml";
    public override string? ImageProperty { get; set; } = "ImageUrl";
    public override int ImageWidth { get; set; } = 512;
}

public class ExperienceController : GenericController<Experience> 
{
    public override string? ImageProperty { get; set; } = "ImageUrl";
    public override int ImageWidth { get; set; } = 512;
    public override string EditView { get; set; } = "Views/Dashboard/Edit/Experience.cshtml";
}
