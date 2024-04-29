using System.ComponentModel.DataAnnotations;

namespace fullstack_portfolio.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; }

    public LoginViewModel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
}
