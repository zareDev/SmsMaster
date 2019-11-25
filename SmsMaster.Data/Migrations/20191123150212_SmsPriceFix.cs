using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsMaster.Data.Migrations
{
    public partial class SmsPriceFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Sms",
                type: "decimal(6, 3)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerSms",
                table: "Country",
                type: "decimal(6, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 3)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Sms",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerSms",
                table: "Country",
                type: "decimal(6, 3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6, 3)");
        }
    }
}
