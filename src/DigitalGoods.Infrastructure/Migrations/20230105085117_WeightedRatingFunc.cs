using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WeightedRatingFunc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(WeightedRatingFunc),
                "20230105085117_WeightedRatingFunc.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION f_weighted_rating");
        }
    }
}
