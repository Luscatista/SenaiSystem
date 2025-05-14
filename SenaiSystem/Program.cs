var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

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

app.UseSwaggerUI();


app.Run();
