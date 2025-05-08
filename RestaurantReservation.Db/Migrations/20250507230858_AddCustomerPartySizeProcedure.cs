using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerPartySizeProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_GetCustomersWithLargePartySize
                    @MinPartySize INT
                AS
                BEGIN
                    SELECT DISTINCT c.*
                    FROM Customers c
                    INNER JOIN Reservations r ON c.customer_id = r.customer_id
                    WHERE r.party_size > @MinPartySize
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetCustomersWithLargePartySize");

        }
    }
}
