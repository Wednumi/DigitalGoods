using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterCodeReserveProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(AlterCodeReserveProc),
                "20230102075618_AlterCodeReserveProcUp.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationUtility.ReadSql(typeof(AlterCodeReserveProc),
                "20230102075618_AlterCodeReserveProcDown.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
