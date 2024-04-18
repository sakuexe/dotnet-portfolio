using fullstack_portfolio.Data;

namespace fullstack_portfolio.Models.ViewModels;

public class HomeViewModel
{
    public List<Expertise> Expertises { get; set; }

    public HomeViewModel()
    {
        Expertises = MongoContext.GetAll<Expertise>();
    }
}
