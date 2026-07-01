using Microsoft.EntityFrameworkCore;
using UserDirectory.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure DB path
var dataDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "app.db");

// Use configuration override if provided
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       $"Data Source={dbPath}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Optional: JWT authentication (commented by default). To enable, configure in appsettings and uncomment.
// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options => {
//         options.Authority = builder.Configuration["Auth:Authority"];
//         options.Audience = builder.Configuration["Auth:Audience"];
//         options.RequireHttpsMetadata = false;
//     });
// builder.Services.AddAuthorization();

var app = builder.Build();

// Ensure DB created and apply migrations
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

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
