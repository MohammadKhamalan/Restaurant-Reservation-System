using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    public class RepositoryTests
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryTests(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAllTests()
        {
            await ListManagers();
            await GetReservationsByCustomer(1);
            await CalculateAverageOrderAmount(1);
            await ListOrdersAndMenuItems(1);
            await ListOrderedMenuItems(1);
            await TestCustomerCrudOperations();
            await TestEmployeeCrudOperations();
            await TestOrderCrudOperations();
            await TestOrderItemCrudOperations();
            await TestMenuItemCrudOperations();
            await TestReservationCrudOperations();
            await TestRestaurantCrudOperations();
            await TestTableCrudOperations();
            await TestReservationWithDetailsAsync();
            await TestEmployeesWithRestaurantsAsync();
        }

        private async Task ListManagers()
        {
            var employeeRepo = _serviceProvider.GetRequiredService<IEmployeeRepository>();
            var managers = await employeeRepo.ListManagersAsync();

            Console.WriteLine("Managers:");
            foreach (var manager in managers)
            {
                Console.WriteLine($"- {manager.FirstName} {manager.LastName}");
            }
            Console.WriteLine();
        }

        private async Task GetReservationsByCustomer(int customerId)
        {
            var reservationRepo = _serviceProvider.GetRequiredService<IReservationRepository>();
            var reservations = await reservationRepo.GetReservationsByCustomer(customerId);

            Console.WriteLine($"Reservations for Customer ID {customerId}:");
            foreach (var r in reservations)
            {
                Console.WriteLine($"- Reservation ID: {r.ReservationId}, Date: {r.ReservationDate}, Restaurant: {r.Restaurant?.Name}");
            }
            Console.WriteLine();
        }

        private async Task CalculateAverageOrderAmount(int employeeId)
        {
            var orderRepo = _serviceProvider.GetRequiredService<IOrderRepository>();
            var average = await orderRepo.CalculateAverageOrderAmount(employeeId);

            Console.WriteLine($"Average order amount by employee ID {employeeId}: {average}");
            Console.WriteLine();
        }

        private async Task ListOrdersAndMenuItems(int reservationId)
        {
            var orderRepo = _serviceProvider.GetRequiredService<IOrderRepository>();
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

        private async Task ListOrderedMenuItems(int reservationId)
        {
            var menuItemRepo = _serviceProvider.GetRequiredService<IMenuItemRepository>();
            var orderedItems = await menuItemRepo.ListOrderedMenuItems(reservationId);

            Console.WriteLine($"Ordered menu items for reservation ID {reservationId}:");
            foreach (var item in orderedItems)
            {
                Console.WriteLine($"- {item.Name} | Price: {item.Price}");
            }
            Console.WriteLine();
        }
        private async Task TestCustomerCrudOperations()
        {
            var customerRepo = _serviceProvider.GetRequiredService<ICustomerRepository>();

           
            var newCustomer = new Customer
            {
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                PhoneNumber="0598168640"
            };

            await customerRepo.AddAsync(newCustomer);
            Console.WriteLine("Added customer.");

            var allCustomers = await customerRepo.GetAllAsync();
            Console.WriteLine($"Total customers: {allCustomers.Count()}");

            var addedCustomer = allCustomers.Last();
            var retrievedCustomer = await customerRepo.GetByIdAsync(addedCustomer.CustomerId);
            Console.WriteLine($"Retrieved: {retrievedCustomer?.FirstName} {retrievedCustomer?.LastName}");

                retrievedCustomer.FirstName = "UpdatedName";
                await customerRepo.UpdateAsync(retrievedCustomer);
                Console.WriteLine("Customer updated.");
            
                await customerRepo.DeleteAsync(retrievedCustomer.CustomerId);
                Console.WriteLine("Customer deleted.");
   
            var deleted = await customerRepo.GetByIdAsync(retrievedCustomer!.CustomerId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }
        private async Task TestEmployeeCrudOperations()
        {
            var EmployeeRepo = _serviceProvider.GetRequiredService<IEmployeeRepository>();

            var newEmployee = new Employee
            {
                RestaurantId = 2,
                FirstName = "Hiba",
                LastName = "Kurd",
                Position = "Manager"

            };

            await EmployeeRepo.AddAsync(newEmployee);
            Console.WriteLine("Added Employee.");
            var allEmployees = await EmployeeRepo.GetAllAsync();
            Console.WriteLine($"Total Employees: {allEmployees.Count()}");

            
            var addedEmployee = allEmployees.Last(); 
            var retrievedEmployee = await EmployeeRepo.GetByIdAsync(addedEmployee.EmployeeId);
            Console.WriteLine($"Retrieved: {retrievedEmployee?.FirstName} {retrievedEmployee?.LastName}");

                retrievedEmployee.LastName = "Al-Kurd";
                await EmployeeRepo.UpdateAsync(retrievedEmployee);
                Console.WriteLine("Employee updated.");
                await EmployeeRepo.DeleteAsync(retrievedEmployee.EmployeeId);
                Console.WriteLine("Employee deleted.");
            var deleted = await EmployeeRepo.GetByIdAsync(retrievedEmployee!.EmployeeId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }
        private async Task TestMenuItemCrudOperations()
        {
            var menuItemRepo = _serviceProvider.GetRequiredService<IMenuItemRepository>(); 
            var newMenuItem = new MenuItem { RestaurantId = 1, Name = "Pasta", Description = "Delicious spaghetti", Price = 12.99M };
            await menuItemRepo.AddAsync(newMenuItem);
            Console.WriteLine("MenuItem created!");

            newMenuItem.Price = 14.99M;
            await menuItemRepo.UpdateAsync(newMenuItem);
            Console.WriteLine("MenuItem updated!");

            var retrievedMenuItem = await menuItemRepo.GetByIdAsync(newMenuItem.ItemId);
            Console.WriteLine($"Retrieved MenuItem: {retrievedMenuItem.Name} - ${retrievedMenuItem.Price}");

            await menuItemRepo.DeleteAsync(newMenuItem.ItemId);
            Console.WriteLine("MenuItem deleted! ");
            var deleted = await menuItemRepo.GetByIdAsync(retrievedMenuItem!.ItemId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }

        private async Task TestOrderCrudOperations()
        {
            var orderRepo = _serviceProvider.GetRequiredService<IOrderRepository>();
            var newOrder = new Order { EmployeeId = 2, ReservationId = 2, OrderDate = DateTime.Now, TotalAmount = 50 };
            await orderRepo.AddAsync(newOrder);
            Console.WriteLine("Order created!");

            newOrder.TotalAmount = 75;
            await orderRepo.UpdateAsync(newOrder);
            Console.WriteLine("Order updated!");

            var retrievedOrder = await orderRepo.GetByIdAsync(newOrder.OrderId);
            Console.WriteLine($"Retrieved Order ID: {retrievedOrder.OrderId} - Total: ${retrievedOrder.TotalAmount}");

            await orderRepo.DeleteAsync(newOrder.OrderId);
            Console.WriteLine("Order deleted! ");
            var deleted = await orderRepo.GetByIdAsync(retrievedOrder!.OrderId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }

        private async Task TestOrderItemCrudOperations()
        {
            var orderItemRepo = _serviceProvider.GetRequiredService<IOrderItemRepository>();
            var newOrderItem = new OrderItem { ItemId = 1, OrderId = 1, Quantity = 2 };
            await orderItemRepo.AddAsync(newOrderItem);
            Console.WriteLine("OrderItem created!");

            newOrderItem.Quantity = 3;
            await orderItemRepo.UpdateAsync(newOrderItem);
            Console.WriteLine("OrderItem updated!");

            var retrievedOrderItem = await orderItemRepo.GetByIdAsync(newOrderItem.OrderItemId);
            Console.WriteLine($"Retrieved OrderItem: Item ID: {retrievedOrderItem.ItemId}, Quantity: {retrievedOrderItem.Quantity}");

            await orderItemRepo.DeleteAsync(newOrderItem.OrderItemId);
            Console.WriteLine("OrderItem deleted! ");
            var deleted = await orderItemRepo.GetByIdAsync(retrievedOrderItem!.OrderItemId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }
        private async Task TestTableCrudOperations()
        {
            var tableRepo = _serviceProvider.GetRequiredService<ITableRepository>();
            var newTable = new Table { RestaurantId = 2, Capacity = 10 };
            await tableRepo.AddAsync(newTable);
            Console.WriteLine("Table created!");

            newTable.Capacity = 8;
            await tableRepo.UpdateAsync(newTable);
            Console.WriteLine("Table updated!");

            var retrievedTable = await tableRepo.GetByIdAsync(newTable.TableId);
            Console.WriteLine($"Retrieved Table: Capacity: {retrievedTable.Capacity}");

            await tableRepo.DeleteAsync(newTable.TableId);
            Console.WriteLine("Table deleted!");
            var deleted = await tableRepo.GetByIdAsync(retrievedTable!.TableId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }



        private async Task TestReservationCrudOperations()
        {
            var reservationRepo = _serviceProvider.GetRequiredService<IReservationRepository>();
            var newReservation = new Reservation { CustomerId = 1, RestaurantId = 2, TableId = 1, PartySize = 4, ReservationDate = DateTime.Now };
            await reservationRepo.AddAsync(newReservation);
            Console.WriteLine("Reservation created!");

            newReservation.PartySize = 5;
            await reservationRepo.UpdateAsync(newReservation);
            Console.WriteLine("Reservation updated!");

            var retrievedReservation = await reservationRepo.GetByIdAsync(newReservation.ReservationId);
            Console.WriteLine($"Retrieved Reservation: Party Size: {retrievedReservation.PartySize}");

            await reservationRepo.DeleteAsync(newReservation.ReservationId);
            Console.WriteLine("Reservation deleted! ");
            var deleted = await reservationRepo.GetByIdAsync(retrievedReservation!.ReservationId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }

        private async Task TestRestaurantCrudOperations()
        {
            var restaurantRepo = _serviceProvider.GetRequiredService<IRestaurantRepository>();
            var newRestaurant = new Restaurant { Name = "Shawerma Restaurant", Address = "15 Food St.", PhoneNumber = "123-456-7890", OpenHours = "9 AM - 10 PM" };
            await restaurantRepo.AddAsync(newRestaurant);
            Console.WriteLine("Restaurant created!");

            newRestaurant.Name = "The Amazing Restaurant";
            await restaurantRepo.UpdateAsync(newRestaurant);
            Console.WriteLine("Restaurant updated!");

            var retrievedRestaurant = await restaurantRepo.GetByIdAsync(newRestaurant.RestaurantId);
            Console.WriteLine($"Retrieved Restaurant: {retrievedRestaurant.Name}, Address: {retrievedRestaurant.Address}");

            await restaurantRepo.DeleteAsync(newRestaurant.RestaurantId);
            Console.WriteLine("Restaurant deleted! ");
            var deleted = await restaurantRepo.GetByIdAsync(retrievedRestaurant!.RestaurantId);
            Console.WriteLine(deleted == null ? "Delete confirmed." : "Delete failed.");
        }

        private async Task TestReservationWithDetailsAsync()
        {
            var reservationRepo = _serviceProvider.GetRequiredService<IReservationRepository>();
            
            var reservations = await reservationRepo.GetReservationsWithDetailsAsync();

            if (reservations != null)
            {
                Console.WriteLine("\nReservations retrieved successfully!");
                foreach (var reservation in reservations)
                {
                    Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Date: {reservation.ReservationDate}, Party Size: {reservation.PartySize}, Customer: {reservation.CustomerFirstName} {reservation.CustomerLastName}, Restaurant: {reservation.RestaurantName}");
                }
            }
            else
            {
                Console.WriteLine("No reservations found.");
            }
        }

        private async Task TestEmployeesWithRestaurantsAsync()
        {
            var employeeRepo = _serviceProvider.GetRequiredService<IEmployeeRepository>();
            var employees = await employeeRepo.GetEmployeesWithRestaurantsAsync();

            if (employees != null)
            {
                Console.WriteLine("\nEmployees retrieved successfully!");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"EmployeeID: {employee.EmployeeId} ,EmployeeName: {employee.EmployeeFirstName} {employee.EmployeeLastName},EmployeePosition: {employee.EmployeePosition}, Restaurant: {employee.RestaurantName}");
                }
            }
            else
            {
                Console.WriteLine("No employees found.");
            }
        }

    }

}
