using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using service_account.Context;
using service_account.Domain;
using service_account.Repositories;
using service_account.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var authServer = builder.Configuration["Authserver"];

builder.Services.AddAuthentication("token")
          .AddJwtBearer("token", options =>
          {
              options.Authority = authServer;
              options.TokenValidationParameters.ValidateAudience = false;
              options.RequireHttpsMetadata = false;
          });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase(connectionString));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Service Account",
            Description = "API to manager account transactions",
            Version = "v1"
        });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(authServer + "/connect/authorize", UriKind.Absolute),
                TokenUrl = new Uri(authServer + "/connect/token", UriKind.Absolute),
                Scopes = new Dictionary<string, string>
                            {
                                { "accountapi", "Account API" }
                            }
            }
        }
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "accountapi", "Account API" }
                    }
                });


    

});

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddTransient<IAccountDomain, AccountDomain>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

 
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Account");
    c.OAuthClientId("swagger-local");
    c.OAuthClientSecret("511536EF-F270-4058-80CA-1C89C192F69A");
    c.OAuthAppName("accountapi");
    c.OAuthScopeSeparator(" ");
    c.OAuthUsePkce();
});

app.Run();

