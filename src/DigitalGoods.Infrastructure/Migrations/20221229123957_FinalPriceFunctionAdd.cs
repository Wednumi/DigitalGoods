using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalPriceFunctionAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(FinalPriceFunctionAdd),
                "20221229123957_FinalPriceFunctionAdd.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION f_calculate_final_price");
        }
    }
}
