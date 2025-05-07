using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Models.Views;
using RestaurantReservation.Db.Seeding;
namespace RestaurantReservation.Db.Data;

public class RestaurantReservationDbContext : DbContext
{
    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
         : base(options) { }

    
    public RestaurantReservationDbContext() { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<ReservationsDetails> ReservationWithDetails { get; set; }
    public DbSet<EmployeesWithRestaurantDetails> EmployeesWithRestaurants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
        if (!optionsBuilder.IsConfigured)
        {
           
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RestaurantReservation"))
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("RestaurantReservationDb");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string 'DefaultConnection' was not found in AppSettings.json.");
            }

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantReservationDbContext).Assembly);
        RestaurantReservation.Db.Seeding.DataSeeding.Seed(modelBuilder);
    }

}