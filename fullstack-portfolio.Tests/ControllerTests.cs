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
    }
}
