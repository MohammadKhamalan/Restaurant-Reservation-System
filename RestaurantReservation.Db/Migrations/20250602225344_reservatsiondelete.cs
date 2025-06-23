using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class reservatsiondelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reservations_reservation_id",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reservations_reservation_id",
                table: "Orders",
                column: "reservation_id",
                principalTable: "Reservations",
                principalColumn: "reservation_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reservations_reservation_id",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reservations_reservation_id",
                table: "Orders",
                column: "reservation_id",
                principalTable: "Reservations",
                principalColumn: "reservation_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
