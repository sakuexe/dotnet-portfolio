using fullstack_portfolio.Data;

namespace fullstack_portfolio.Models.ViewModels;

public class HomeViewModel
{
    public Expertise[] Expertises { get; set; }
    public Skill[] Skills { get; set; }
    public Project[] Portfolio { get; set; }
    public Experience[] Experience { get; set; }
    public ContactsViewModel ContactsViewModel { get; set; }

    public HomeViewModel()
    {
        Expertises = MongoContext.GetAll<Expertise>().GetAwaiter().GetResult().ToArray();
        Skills = MongoContext.GetAll<Skill>().GetAwaiter().GetResult().ToArray();
        Portfolio = MongoContext.GetAll<Project>().GetAwaiter().GetResult().ToArray();
        Experience = MongoContext.GetAll<Experience>().GetAwaiter().GetResult().ToArray();
        ContactsViewModel = new ContactsViewModel();
    }
}
