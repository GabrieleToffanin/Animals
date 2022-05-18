using Animals.Core.Interfaces;
using Animals.Core.Logic;
using Animals.Core.Models;
using Animals.Core.Services;
using Animals.EF.Data;
using Animals.EF.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnimalDbContext>(
    options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test", 
    migrationASM => migrationASM.MigrationsAssembly("Animals.Api")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IMappingService, MappingService>();
builder.Services.AddScoped<IMainBusinessLogic, AnimalsBusinessLogic>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<ISpecieRepository, SpecieRepository>();
builder.Services.AddScoped<ISpecieBusinessLogic, SpeciesBusinessLogic>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
