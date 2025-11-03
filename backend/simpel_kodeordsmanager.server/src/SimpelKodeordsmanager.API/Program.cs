using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SimpelKodeordsmanager.API.Controllers.Shared;
using SimpelKodeordsmanager.API.Middlewares;
using SimpelKodeordsmanager.Application;
using SimpelKodeordsmanager.Domain.Models;
using SimpelKodeordsmanager.Infrastructure;
using SimpelKodeordsmanager.Persistence;

using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Starting Simpel Kodeordsmanager API");

var builder = WebApplication.CreateBuilder(args);

// Load and bind configuration
var configuration = builder.Configuration;
// var jwt = configuration.GetSection("Jwt").Get<Jwt>()!;
var keycloak = configuration.GetSection("Keycloak").Get<Keycloak>()!;

// Set Danish culture globally
var cultureInfo = new CultureInfo("da-DK");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


// Global Exception Handler
builder.Services.AddProblemDetails(configure =>
{
    configure.CustomizeProblemDetails = context => { context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier; };
});

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

// Add Application services to the container.
builder.Services
    .AddApplication(configuration)
    .AddInfrastructure(configuration)
    .AddPersistence(configuration);

// Add Authentication and Authorization
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.Audience = keycloak.Audience;
        options.MetadataAddress = keycloak.MetadataAddress;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = keycloak.ValidIssuer,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = false,
            ValidateAudience = true,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Roles.All, policy =>
        policy.RequireRole(Roles.Admin, Roles.Member));

builder.Services.AddTransient<IClaimsTransformation, RoleClaimsTransformation>();

builder.Services.AddHttpContextAccessor();

// Add Swqagger
builder.Services
    .AddOpenApi()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Kodeordsmanager API",
            Description = "ASP.NET Core Web API til håndtering af kodeord",
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath);
    });

// Add Controllers
builder.Services.AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new LowerCaseParameterTransformer())); });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Middleware
app.UseMiddleware<ExceptionMiddleware>();

// redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable routing, needed when using controllers 
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// CORS configuration
app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE");
    x.WithOrigins("http://localhost:4200", "https://kodeordsmanager.k7c.dk");
    x.AllowCredentials();
});

// Use controllers as endpoints
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


// Run the application
try
{
    app.Run();
}
catch (Exception e)
{
    Log.Error("Application error: {Message}", e.Message);
}
finally
{
    Log.CloseAndFlush();
}