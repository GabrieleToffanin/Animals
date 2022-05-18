using Animals.Core.Interfaces;
using Animals.Core.Logic;
using Animals.Core.Models;
using Animals.Core.Models.User;
using Animals.Core.Services;
using Animals.EF.Data;
using Animals.EF.Data.Seeding;
using Animals.EF.Repository;
using Microsoft.AspNetCore.Identity;
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
    options.UseSqlServer(builder.Configuration["ConnectionStrings:LocalSqlServer"],
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

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ApplicationDbContextSeed.SeedEssentialAsync(userManager, roleManager);

    }
    catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured seeding the DB.");
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller}/{action}/{id?}");

app.Run();
