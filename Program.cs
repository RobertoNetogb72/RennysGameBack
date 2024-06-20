using Microsoft.EntityFrameworkCore;
using backend.Context;

var builder = WebApplication.CreateBuilder(args);

// Add MySQL database
builder.Services.AddDbContext<FinalDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConexaoPadrao");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


//Adicinado Controllers
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

//Adicionado
app.UseAuthorization();

//Adicionado
app.MapControllers();

app.Run();