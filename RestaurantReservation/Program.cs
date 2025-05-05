using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Data;
namespace RestaurantReservation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up dependency injection
            var services = new ServiceCollection();
            services.AddDbContext<RestaurantReservationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RestaurantReservationDb")));

            var serviceProvider = services.BuildServiceProvider();

           

            Console.WriteLine("Database setup complete. Ready to test functionality.");
        }
    }
}
