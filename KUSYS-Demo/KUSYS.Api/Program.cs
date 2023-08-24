using KUSYS.Api.Infrastructure;
using KUSYS.Core.Authentication.Model;
using KUSYS.Core.Infrastructure;
using KUSYS.DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddDbContext<KUSYSDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.InjectionRegisters();
builder.Services.AddControllers();

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

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "KUSYS Demo Api", Version = "v1" });
    setup.AddSecurityDefinition("Bearer Token", new OpenApiSecurityScheme
    {
        Description = "<YOUR JWT TOKEN>",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    //setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference { Id = string.Empty, Type = ReferenceType.SecurityScheme }
    //        },
    //        new string[] { }
    //    }
    //});
});

builder.Services.AddExceptionHandler(options => new ExceptionHandlerMiddleware(options.ExceptionHandler));
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        options.DefaultModelsExpandDepth(-1);
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "KUSYS Demo Api v1");
    });
}
//app.UseCors(x => x
//   .AllowAnyOrigin()
//   .AllowAnyMethod()
//   .AllowAnyHeader());

app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandlerMiddleware();

app.Use((context, middleware) =>
{
    //SeedDataManager.SeedJobs(context);
    return middleware(context);
});
app.MapControllers();

app.Run();
