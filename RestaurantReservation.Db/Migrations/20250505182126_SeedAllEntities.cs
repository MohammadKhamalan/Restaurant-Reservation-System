using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "customer_id", "Email", "First_Name", "Last_Name", "Phone_Number" },
                values: new object[,]
                {
                    { 1, "mohammad.khamalan@gmail.com", "Mohammad", "Khamalan", "0598168640" },
                    { 2, "Hiba.kurd@foothill.com", "Hiba", "Kurd", "987654321" },
                    { 3, "saif.fares@example.com", "Saif", "fares", "555666777" },
                    { 4, "adnan.masri@example.com", "Adnan", "Masri", "999888777" },
                    { 5, "wafa.khamalan@example.com", "Wafa", "Khamalan", "111222333" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "restaurant_id", "Address", "Name", "opening_hours", "phone_number" },
                values: new object[,]
                {
                    { 1, "15 street", "Pizza Time", "9 AM - 9 PM", "555-1234" },
                    { 2, "16 street", "Yalla Pasta", "11 AM - 10 PM", "555-5678" },
                    { 3, "Rafidia", "Orgada Burgar", "8 AM - 8 PM", "555-9876" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "employee_id", "first_name", "last_name", "position", "restaurant_id" },
                values: new object[,]
                {
                    { 1, "Mark", "Johnson", "Manager", 1 },
                    { 2, "Sara", "Williams", "Waiter", 2 },
                    { 3, "Tom", "Lee", "Chef", 1 },
                    { 4, "Nancy", "Davis", "Waiter", 3 },
                    { 5, "Jake", "Wilson", "Manager", 2 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "item_id", "description", "name", "price", "restaurant_id" },
                values: new object[,]
                {
                    { 1, "Cheese Pizza", "Pizza", 40.0m, 1 },
                    { 2, "Beef Burger", "Burger", 28.0m, 1 },
                    { 3, "Spaghetti Bolognese", "Pasta", 12.0m, 2 },
                    { 4, "Caesar Salad", "Salad", 9.0m, 3 },
                    { 5, "Tomato Soup", "Soup", 3.0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "table_id", "capacity", "restaurant_id" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 1 },
                    { 3, 6, 2 },
                    { 4, 4, 2 },
                    { 5, 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "reservation_id", "customer_id", "party_size", "reservation_date", "restaurant_id", "table_id" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 2, 6, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 3, 3, 4, new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5 },
                    { 4, 4, 2, new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 5, 5, 4, new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "order_id", "employee_id", "order_date", "reservation_id", "total_amount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 150 },
                    { 2, 2, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 260 },
                    { 3, 3, new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 270 },
                    { 4, 4, new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 540 },
                    { 5, 5, new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 130 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "order_item_id", "item_id", "order_id", "quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 2, 3 },
                    { 4, 4, 3, 2 },
                    { 5, 5, 4, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "order_item_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "item_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "order_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservation_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "employee_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservation_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservation_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservation_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "reservation_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "customer_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "table_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "restaurant_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "restaurant_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "restaurant_id",
                keyValue: 3);
        }
    }
}
