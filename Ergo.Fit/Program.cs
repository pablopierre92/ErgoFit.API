using Ergo.Fit.Configuration;
using Ergo.Fit.DataContext;
using Ergo.Fit.Service.DepartamentoService;
using Ergo.Fit.Service.EmpresaService;
using Ergo.Fit.Service.FuncionarioService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicionar OpenAPI (nova forma do .NET 9)
builder.Services.AddOpenApi();

// Adicionar Swagger UI para visualizar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>();
builder.Services.AddScoped<IEmpresaInterface, EmpresaService>();
builder.Services.AddScoped<IDepartamentoInterface, DepartamentoService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // OpenAPI endpoint (JSON)
    app.MapOpenApi();

    // Swagger UI (interface visual)
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ErgoFit API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();