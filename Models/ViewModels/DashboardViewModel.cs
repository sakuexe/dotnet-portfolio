using System.Reflection;
using fullstack_portfolio.Controllers;

namespace fullstack_portfolio.Models.ViewModels;

public class DashboardViewModel
{
    public List<string> Collections { get; set; } = new();

    public DashboardViewModel()
    {
        var baseClass = typeof(GenericController<>);
        // fetch controllers that inherit from GenericController
        Assembly asm = Assembly.GetExecutingAssembly();
        var controllers = asm.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null)
            .Where(t => t.BaseType!.IsGenericType && t.BaseType.GetGenericTypeDefinition() == baseClass)
            .Select(t => t.BaseType!.GetGenericArguments()[0].Name)
            .ToList();
        Collections = controllers;
    }
}
