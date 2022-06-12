using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(config => {
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
        config.SignedOutCallbackPath = "/Home/Index";


        // configure cookie claim mapping
        config.ClaimActions.DeleteClaim("amr");
        config.ClaimActions.DeleteClaim("s_hash");
        config.ClaimActions.MapUniqueJsonKey("RawCoding.Grandma", "rc.garndma");

        // two trips to load claims in to the cookie
        // but the id token is smaller !
        config.GetClaimsFromUserInfoEndpoint = true;

        // configure scope
        config.Scope.Clear();
        config.Scope.Add("openid");
        config.Scope.Add("rc.scope");
        config.Scope.Add("ApiOne");
        config.Scope.Add("ApiTwo");
        config.Scope.Add("offline_access");
    });
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews(); 
var app = builder.Build();

app.UseAuthentication();    
app.UseAuthorization(); 

app.MapDefaultControllerRoute();    

app.Run();
