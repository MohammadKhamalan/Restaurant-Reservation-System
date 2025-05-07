using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation;
using Microsoft.EntityFrameworkCore;
namespace RestaurantReservation
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<RestaurantReservationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RestaurantReservationDb")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>(); 
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            var serviceProvider = services.BuildServiceProvider();

            var tests = new RepositoryTests(serviceProvider);
            await tests.RunAllTests();
        }
    }
}