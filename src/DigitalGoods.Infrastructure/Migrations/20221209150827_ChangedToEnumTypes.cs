using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGoods.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToEnumTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_ReceiveMethods_ReceiveMethodId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "BankAccountTypes");

            migrationBuilder.DropTable(
                name: "ReceiveMethods");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ReceiveMethodId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_BankAccountTypeId",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "ReceiveMethodId",
                table: "Offers",
                newName: "ReceiveMethod");

            migrationBuilder.RenameColumn(
                name: "BankAccountTypeId",
                table: "BankAccounts",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveMethod",
                table: "Offers",
                newName: "ReceiveMethodId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "BankAccounts",
                newName: "BankAccountTypeId");

            migrationBuilder.CreateTable(
                name: "BankAccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ReceiveMethodId",
                table: "Offers",
                column: "ReceiveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_BankAccountTypes_BankAccountTypeId",
                table: "BankAccounts",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_ReceiveMethods_ReceiveMethodId",
                table: "Offers",
                column: "ReceiveMethodId",
                principalTable: "ReceiveMethods",
                principalColumn: "Id");
        }
    }
}
