using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRatingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(UpdateRatingFix),
                "20230104075343_UpdateRatingFixUp.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(UpdateRatingFix),
                "20230104075343_UpdateRatingFixDown.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
