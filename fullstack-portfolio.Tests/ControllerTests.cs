using fullstack_portfolio.Controllers;
using fullstack_portfolio.Models;

namespace fullstack_portfolio.Tests;

public class GenericControllerTest
{
    [Fact]
    public void WorkingController()
    {
        var userController = new GenericController<Skill>();
        Assert.NotNull(userController);
        // check that the controller has the necessary methods
        // do not invoke them though
        Assert.NotNull(userController.Index());
        Assert.NotNull(userController.New());
        Assert.NotNull(userController.Edit("1"));
        Assert.NotNull(userController.Save(new Skill()));
    }
}
