
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar los servicios.
builder.Services.AddControllers();

// Configurar OpenAPI.
builder.Services.AddOpenApi();

// Conectar con SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurar OpenAPI en desarrollo.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
