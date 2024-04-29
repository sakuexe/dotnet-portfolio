using System.Security.Claims;
using fullstack_portfolio.Data;
using fullstack_portfolio.Models;
using fullstack_portfolio.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_portfolio;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        // if the user is already authenticated, redirect to the home page
        if (User.Identity?.IsAuthenticated == true)
            return Redirect("/");
        ViewBag.Title = "Login";
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Title = "Login";
            return View(model);
        }

        // authenticate the user
        if (await AuthenticateUser(model.Username, model.Password) == false)
        {
            ModelState.AddModelError("Username", "Invalid username or password");
            ViewBag.Title = "Login";
            return View(model);
        }

        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }

    private async Task<bool> AuthenticateUser(string username, string password, int expiresMinutes = 30)
    {
        List<User> users = await MongoContext.GetAll<User>();
        if (users.Count == 0)
            return false;
        User? user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
            return false;

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
		};

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            // if user is still on site, the cookie will be refreshed
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(expiresMinutes), 
            IsPersistent = true,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return true;
    }
}
