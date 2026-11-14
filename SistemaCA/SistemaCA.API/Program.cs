using Microsoft.EntityFrameworkCore;
using SistemaCA.Infrastructure.Context;
using SistemaCA.Infrastructure.Interfaces;
using SistemaCA.Infrastructure.Repositories;
using SistemaCA.Application.Contract;
using SistemaCA.Application.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conexion = builder.Configuration.GetConnectionString("DefaultConnection");

// Conexión con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(conexion));
// Registro de repositorios
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IConsolaRepository, ConsolaRepository>();
        builder.Services.AddScoped<IAlquilerRepository, AlquilerRepository>();
        builder.Services.AddScoped<IPagoRepository, PagoRepository>();

// Registro de servicios
        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<IConsolaService, ConsolaService>();
        builder.Services.AddScoped<IAlquilerService, AlquilerService>();
        builder.Services.AddScoped<IPagoService, PagoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();