using fullstack_portfolio.Data;

namespace fullstack_portfolio.Models.ViewModels;

public class HomeViewModel
{
    public Expertise[] Expertises { get; set; }
    public Skill[] Skills { get; set; }

    public HomeViewModel()
    {
        Expertises = MongoContext.GetAll<Expertise>().ToArray();
        Skills = MongoContext.GetAll<Skill>().ToArray();
    }
}
