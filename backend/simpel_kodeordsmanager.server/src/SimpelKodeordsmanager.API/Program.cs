using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SimpelKodeordsmanager.Application;
using SimpelKodeordsmanager.Domain.Models;
using SimpelKodeordsmanager.Infrastructure;
using SimpelKodeordsmanager.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Load and bind configuration
var configuration = builder.Configuration;
var jwt = configuration.GetSection("Jwt").Get<JwtModel>()!;

// Set Danish culture globally
var cultureInfo = new CultureInfo("da-DK");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add Application services to the container.
builder.Services
    .AddApplication(configuration)
    .AddInfrastructure(configuration)
    .AddPersistence(configuration);

// Add Logging
builder.Logging.AddSerilog(new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger());

// Add Authentication and Authorization
builder.Services
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var rsaKey = RSA.Create();
        rsaKey.ImportFromPem(File.ReadAllText(jwt.PrivateKeyPath));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new RsaSecurityKey(rsaKey),
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
        };
    });

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
builder.Services
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

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