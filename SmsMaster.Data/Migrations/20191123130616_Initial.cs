using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsMaster.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    MobileCountryCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    PricePerSms = table.Column<decimal>(type: "decimal(6, 3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.MobileCountryCode);
                });

            migrationBuilder.CreateTable(
                name: "Sms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    From = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Mcc = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "MobileCountryCode", "CountryCode", "Name", "PricePerSms" },
                values: new object[] { "262", "49", "Germany", 0.055m });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "MobileCountryCode", "CountryCode", "Name", "PricePerSms" },
                values: new object[] { "232", "43", "Austria", 0.053m });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "MobileCountryCode", "CountryCode", "Name", "PricePerSms" },
                values: new object[] { "260", "48", "Poland", 0.032m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Sms");
        }
    }
}
