using IdentityServer;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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

var assembly = typeof(Program).Assembly.GetName().Name;

//var filePath = Path.Combine(_env.ContentRootPath, "is_cert.pfx");
//var certificate = new X509Certificate2(filePath, "password");

//builder.Services.AddIdentityServer()
//    .AddAspNetIdentity<IdentityUser>()
//                //.AddConfigurationStore(options =>
//                //{
//                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
//                //        sql => sql.MigrationsAssembly(assembly));
//                //})
//                //.AddOperationalStore(options =>
//                //{
//                //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
//                //        sql => sql.MigrationsAssembly(assembly));
//                //})
//                //.AddDeveloperSigningCredential();
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
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
