using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyFactor.Carbone.Configuration.Migrations
{
    public partial class Product_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CurrentCost = table.Column<decimal>(nullable: true),
                    StandardCost = table.Column<decimal>(nullable: true),
                    QuantityOnHand = table.Column<decimal>(nullable: true),
                    IsStockItem = table.Column<bool>(nullable: false),
                    ConvertToCustomerAsset = table.Column<bool>(nullable: false),
                    Taxable = table.Column<bool>(nullable: false),
                    PurchaseName = table.Column<string>(nullable: true),
                    FieldServiceProductType = table.Column<int>(nullable: false),
                    ProductStructure = table.Column<int>(nullable: false),
                    DecimalPlaces = table.Column<int>(nullable: false),
                    TimeZoneId = table.Column<string>(nullable: true),
                    ValidFromDate = table.Column<DateTime>(nullable: true),
                    ValidToDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Number",
                table: "Products",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
