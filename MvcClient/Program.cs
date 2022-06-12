var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(
    config =>
    {
        config.DefaultScheme = "Cookie";
        config.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookie")
    .AddOpenIdConnect("oidc", config =>
    {
        config.Authority = "https://localhost:7279/";
        config.ClientId = "client_id_mvc";
        config.ClientSecret = "client_secret_mvc";
        config.SaveTokens = true;
        config.ResponseType = "code";
        //config.SignedOutCallbackPath = "/Home/Index";
    });
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseAuthorization();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();