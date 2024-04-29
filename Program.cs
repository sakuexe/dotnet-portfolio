using fullstack_portfolio.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServerSideBlazor(); // Blazor
builder.Services.AddControllersWithViews();

// Add the database context
builder.Services.AddSingleton<MongoContext>();
// access it from anywhere, since the context is a singleton
// and its properties are static

MongoContext.Initialize(builder.Configuration);
MongoContext.SeedDatabase();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.Name = "very_tasty_cookie";
        /* options.AccessDeniedPath = "/Login/Logout"; */
        options.LogoutPath = "/";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Authentication middleware must be added before Authorization middleware
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub(); // Blazor Hub

app.Run();
