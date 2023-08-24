using KUSYS.Core.Infrastructure;
using KUSYS.Core.Models.Authorization;
using KUSYS.DataAccess.Contexts;
using KUSYS.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddDbContext<KUSYSDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.InjectionRegisters();

builder.Services.AddMvcCore(config =>
{
    //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    //config.Filters.Add(new AuthorizeFilter(policy));
    config.Filters.Add(new AuthorizeActionFilter());
});

var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
var securityKey = Convert.FromBase64String(tokenSettings.SecurityKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = tokenSettings.Issuer,
            ValidAudience = tokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(securityKey),
            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddExceptionHandler(options => new ExceptionHandlerMiddleware(options.ExceptionHandler));
builder.Services.AddCors();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromHours(2)); // 2 saat sonra session düþer

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
SeedDataManager.SeedAll(app);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseExceptionHandlerMiddleware();

//app.Use((context, middleware) =>
//{
//    var request = context.Request;
//    var tokenResponse = request.HttpContext.GetAuthUser();
//    var response = context.Response;

//    if (string.IsNullOrEmpty(tokenResponse?.Token) && response.StatusCode == (int)HttpStatusCode.Unauthorized)
//        response.Redirect("/Account/Login");
//    return middleware(context);
//});

//app.UseStatusCodePages(async context =>
//{
//    var request = context.HttpContext.Request;
//    var tokenResponse = request.HttpContext.GetAuthUser();
//    var response = context.HttpContext.Response;

//    if (string.IsNullOrEmpty(tokenResponse?.Token) && response.StatusCode == (int)HttpStatusCode.Unauthorized)
//        response.Redirect("/Account/Login");    
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
