var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicionar OpenAPI (nova forma do .NET 9)
builder.Services.AddOpenApi();

// Adicionar Swagger UI para visualizar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // OpenAPI endpoint (JSON)
    app.MapOpenApi();

    // Swagger UI (interface visual) ← ISSO QUE ESTAVA FALTANDO!
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