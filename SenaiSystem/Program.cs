using Microsoft.IdentityModel.Tokens;
using System.Text;
using SenaiSystem.Interface;
using SenaiSystem.Interfaces;
using SenaiSystem.Repositories;
using SenaiSystem.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()

    // Configuração para lidar com loopings entre tabelas
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<SenaiSystemContext>();

builder.Services.AddTransient<INotaRepository, NotaRepository>();
builder.Services.AddTransient<INotaCategoriaRepository, NotaCategoriaRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ILembreteRepository, LembreteRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateActor = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ecommerce",
            ValidAudience = "ecommerce",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kw%!wZ6rzv9V9yCg9WvZbbJgvs7US8Go%h66E22d"))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: "minhasOrigens",
            policy =>
            {
                //TODO: Alterar link para o Frontend
                policy.WithOrigins("http://localhost:5500");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }
        );
    });

var app = builder.Build();

app.UseCors("minhasOrigens");

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();

app.UseAuthorization();

app.Run();