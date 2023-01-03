using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodeReservingProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ActivationCodes_ActivationCodeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ActivationCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ActivationCodeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Activated",
                table: "ActivationCodes");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ActivationCodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    PayMethod = table.Column<int>(type: "int", nullable: false),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivationCodes_OrderId",
                table: "ActivationCodes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_UserId",
                table: "PaymentRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivationCodes_Orders_OrderId",
                table: "ActivationCodes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            var sql = MigrationUtility.ReadSql(typeof(CodeReservingProcedure),
                "20230102073238_CodeReservingProcedure.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivationCodes_Orders_OrderId",
                table: "ActivationCodes");

            migrationBuilder.DropTable(
                name: "PaymentRecords");

            migrationBuilder.DropIndex(
                name: "IX_ActivationCodes_OrderId",
                table: "ActivationCodes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ActivationCodes");

            migrationBuilder.AddColumn<int>(
                name: "ActivationCodeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activated",
                table: "ActivationCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    Input = table.Column<bool>(type: "bit", nullable: false),
                    PayMethod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ActivationCodeId",
                table: "Orders",
                column: "ActivationCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ActivationCodes_ActivationCodeId",
                table: "Orders",
                column: "ActivationCodeId",
                principalTable: "ActivationCodes",
                principalColumn: "Id");

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS dbo.p_reserve_activation_code");
        }
    }
}
