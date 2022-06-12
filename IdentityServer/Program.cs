using IdentityServer;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(config =>
{
    //config.UseSqlServer(connectionString);
    config.UseInMemoryDatabase("Memory");
});

// AddIdentity registers the services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "IdentityServer.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddDeveloperSigningCredential();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

    var user = new IdentityUser("TruongDV");
    userManager.CreateAsync(user, "Truong@98").GetAwaiter().GetResult();
    userManager.AddClaimAsync(user, new Claim("rc.garndma", "big.cookie"))
        .GetAwaiter().GetResult();
    userManager.AddClaimAsync(user,
        new Claim("rc.api.garndma", "big.api.cookie"))
        .GetAwaiter().GetResult();
}

app.UseIdentityServer();
app.MapDefaultControllerRoute();    
app.Run();
