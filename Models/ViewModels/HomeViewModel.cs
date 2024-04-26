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
        Expertises = MongoContext.GetAll<Expertise>().ToArray();
        Skills = MongoContext.GetAll<Skill>().ToArray();
        Portfolio = MongoContext.GetAll<Project>().ToArray();
        Experience = MongoContext.GetAll<Experience>().ToArray();
        ContactsViewModel = new ContactsViewModel();
    }
}
