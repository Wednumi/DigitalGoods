using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRatingAddedUserUpdating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(UpdateRatingAddedUserUpdating),
                "20230104073307_UpdateRatingAddedUserUpdatingUp.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(UpdateRatingAddedUserUpdating),
                "20230104073307_UpdateRatingAddedUserUpdatingDown.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
