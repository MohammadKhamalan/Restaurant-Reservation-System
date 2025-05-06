using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation
{
    class Program
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
            services.AddScoped<IMenuItemRepository, MenuItemRepository>(); // ✅ Add this for ListOrderedMenuItems

            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Testing database repository methods...\n");

            await ListManagers(serviceProvider);
            await GetReservationsByCustomer(serviceProvider, 1);
            await CalculateAverageOrderAmount(serviceProvider, 1);
            await ListOrdersAndMenuItems(serviceProvider, 1);
            await ListOrderedMenuItems(serviceProvider, 1);
        }

        static async Task ListManagers(IServiceProvider serviceProvider)
        {
            var employeeRepo = serviceProvider.GetRequiredService<IEmployeeRepository>();
            var managers = await employeeRepo.ListManagersAsync();

            Console.WriteLine("Managers:");
            foreach (var manager in managers)
            {
                Console.WriteLine($"- {manager.FirstName} {manager.LastName}");
            }
            Console.WriteLine();
        }

        static async Task GetReservationsByCustomer(IServiceProvider serviceProvider, int customerId)
        {
            var reservationRepo = serviceProvider.GetRequiredService<IReservationRepository>();
            var reservations = await reservationRepo.GetReservationsByCustomer(customerId);

            Console.WriteLine($"Reservations for Customer ID {customerId}:");
            foreach (var r in reservations)
            {
                Console.WriteLine($"- Reservation ID: {r.ReservationId}, Date: {r.ReservationDate}, Restaurant: {r.Restaurant?.Name}");
            }
            Console.WriteLine();
        }

        static async Task CalculateAverageOrderAmount(IServiceProvider serviceProvider, int employeeId)
        {
            var orderRepo = serviceProvider.GetRequiredService<IOrderRepository>();
            var average = await orderRepo.CalculateAverageOrderAmount(employeeId);

            Console.WriteLine($"Average order amount by employee ID {employeeId}: {average}");
            Console.WriteLine();
        }

        static async Task ListOrdersAndMenuItems(IServiceProvider serviceProvider, int reservationId)
        {
            var orderRepo = serviceProvider.GetRequiredService<IOrderRepository>();
            var ordersWithItems = await orderRepo.ListOrdersAndMenuItemsAsync(reservationId);

            Console.WriteLine($"Orders and menu items for reservation ID {reservationId}:");
            foreach (var order in ordersWithItems)
            {
                Console.WriteLine($"- Order ID: {order.OrderId}, Total Amount: {order.TotalAmount}");
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  > Menu Item: {item.MenuItem?.Name}, Quantity: {item.Quantity}");
                }
            }
            Console.WriteLine();
        }

        static async Task ListOrderedMenuItems(IServiceProvider serviceProvider, int reservationId)
        {
            var menuItemRepo = serviceProvider.GetRequiredService<IMenuItemRepository>();
            var orderedItems = await menuItemRepo.ListOrderedMenuItems(reservationId);

            Console.WriteLine($"Ordered menu items for reservation ID {reservationId}:");
            foreach (var item in orderedItems)
            {
                Console.WriteLine($"- {item.Name} | Price: {item.Price}");
            }
            Console.WriteLine();
        }
    }
}
