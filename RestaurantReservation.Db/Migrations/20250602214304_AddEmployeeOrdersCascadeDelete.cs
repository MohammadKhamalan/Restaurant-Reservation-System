using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeOrdersCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_employee_id",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_employee_id",
                table: "Orders",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "employee_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_employee_id",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_employee_id",
                table: "Orders",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "employee_id");
        }
    }
}
