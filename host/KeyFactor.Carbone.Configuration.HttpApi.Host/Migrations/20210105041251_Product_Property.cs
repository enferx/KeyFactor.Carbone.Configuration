using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyFactor.Carbone.Configuration.Migrations
{
    public partial class Product_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultValueDecimal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DefaultValueDouble = table.Column<double>(type: "float", nullable: true),
                    DefaultValueString = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DefaultValueInteger = table.Column<int>(type: "int", nullable: true),
                    MaxLengthString = table.Column<int>(type: "int", nullable: true),
                    MaxDecimalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinDecimalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxDoubleValue = table.Column<double>(type: "float", nullable: true),
                    MinDoubleValue = table.Column<double>(type: "float", nullable: true),
                    DoublePrecission = table.Column<int>(type: "int", nullable: true),
                    MaxIntegerValue = table.Column<int>(type: "int", nullable: true),
                    MinIntegerValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProperties_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperties_ProductId",
                table: "ProductProperties",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProperties");
        }
    }
}
