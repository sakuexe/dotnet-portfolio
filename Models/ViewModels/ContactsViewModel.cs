using fullstack_portfolio.Data;
using fullstack_portfolio.Models;

namespace fullstack_portfolio;

public class ContactsViewModel 
{
    public ContactInfo ContactInfo { get; set; }
    // email form fields
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }

    public ContactsViewModel()
    {
        ContactInfo = MongoContext.GetLatest<ContactInfo>() ?? new ContactInfo();
        Email = string.Empty;
        Name = string.Empty;
        Subject = string.Empty;
        Message = string.Empty;
    }
}
