using Animals.Core.Settings;
using Animals.EF.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Test.Helper
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(service =>
            {
                var descriptor = service.SingleOrDefault(
                    d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

                service.Remove(descriptor);


                service.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                service.AddEntityFrameworkInMemoryDatabase();
                service.AddAuthentication(o => o.DefaultScheme = "JWT")
                .AddJwtBearer("Token", o =>
                {
                    var key = Encoding.UTF8.GetBytes("MagicUnicornsGeneratingKeys");
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

                var sp = service.BuildServiceProvider();

                using(var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTest(db);
                    }
                    catch(Exception exception)
                    {
                        logger.LogError(exception, "An Error occured while seeding the database with animals. Error{Message}", exception.Message);
                    }
                }
            });
        }
    }
}
