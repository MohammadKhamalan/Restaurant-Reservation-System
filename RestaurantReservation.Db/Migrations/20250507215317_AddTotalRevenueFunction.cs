using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalRevenueFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION fn_CalculateRestaurantRevenue(@restaurant_id INT)
                RETURNS DECIMAL(10, 2)
                AS
                BEGIN
                    DECLARE @totalrevenue DECIMAL(10, 2)

                    SELECT @totalrevenue = SUM(o.total_amount)
                    FROM Orders o
                    JOIN Reservations res ON res.reservation_id = o.reservation_id
                    WHERE res.restaurant_id = @restaurant_id

                    RETURN @totalrevenue
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION fn_CalculateRestaurantRevenue");

        }
    }
}
