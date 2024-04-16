using fullstack_portfolio.Data;

namespace fullstack_portfolio.Models.ViewModels;

public class DashboardViewModel
{
    public List<string> Collections = MongoContext.GetCollectionNames();
}
