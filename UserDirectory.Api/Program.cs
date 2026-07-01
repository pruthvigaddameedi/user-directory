using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FluentValidation.AspNetCore;
using UserDirectory.Infrastructure.Data;
using UserDirectory.Infrastructure.Repositories;
using UserDirectory.Application.Interfaces;
using UserDirectory.Application.DTOs;
using UserDirectory.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// DB path and connection
var dataDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "app.db");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       $"Data Source={dbPath}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// Application & Infrastructure DI
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<UserDirectory.Application.UseCases.GetUsers.GetUsersHandler>();
builder.Services.AddScoped<UserDirectory.Application.UseCases.CreateUser.CreateUserHandler>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();

// Authentication (JWT Bearer)
var authSection = builder.Configuration.GetSection("Auth");
var authority = authSection["Authority"];
var audience = authSection["Audience"];
var requireHttps = bool.TryParse(authSection["RequireHttpsMetadata"], out var r) ? r : true;

if (!string.IsNullOrEmpty(authority) && !string.IsNullOrEmpty(audience))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = authority;
        options.Audience = audience;
        options.RequireHttpsMetadata = requireHttps;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                var logger = ctx.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("JwtAuth");
                logger.LogError(ctx.Exception, "Authentication failed");
                return Task.CompletedTask;
            }
        };
    });

    builder.Services.AddAuthorization();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (!string.IsNullOrEmpty(authority) && !string.IsNullOrEmpty(audience))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapControllers();
app.Run();
