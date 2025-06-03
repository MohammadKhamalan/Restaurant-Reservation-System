using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Restaurants_restaurant_id",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Restaurants_restaurant_id",
                table: "Employees",
                column: "restaurant_id",
                principalTable: "Restaurants",
                principalColumn: "restaurant_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Restaurants_restaurant_id",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Restaurants_restaurant_id",
                table: "Employees",
                column: "restaurant_id",
                principalTable: "Restaurants",
                principalColumn: "restaurant_id");
        }
    }
}
