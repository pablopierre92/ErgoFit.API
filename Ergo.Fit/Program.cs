using Ergo.Fit.Configuration;
using Ergo.Fit.DataContext;
using Ergo.Fit.Service.DepartamentoService;
using Ergo.Fit.Service.EmpresaService;
using Ergo.Fit.Service.FuncionarioService;
using Ergo.Fit.Service.TokenService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== ADD SERVICES =====

// Controllers
builder.Services.AddControllers();

// OpenAPI e Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ErgoFit API",
        Version = "v1",
        Description = "API do sistema ErgoFit"
    });

    // Configurar JWT no Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Database Context
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
  //  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

// Identity Configuration
builder.Services.AddIdentityConfiguration(builder.Configuration);

// JWT Configuration
builder.Services.AddJwtConfiguration(builder.Configuration, builder.Environment);

// Application Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>();
builder.Services.AddScoped<IEmpresaInterface, EmpresaService>();
builder.Services.AddScoped<IDepartamentoInterface, DepartamentoService>();

// CORS (para Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ===== SEED ROLES =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}

// ===== CONFIGURE PIPELINE =====

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ErgoFit API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular"); //  Adicione CORS antes de Authentication

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();