using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Categories",
            //    columns: table => new
            //    {
            //        CategoryId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        CategoryName = table.Column<string>(type: "text", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categories", x => x.CategoryId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Oders",
            //    columns: table => new
            //    {
            //        PrderId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        CustomerId = table.Column<string>(type: "text", nullable: false),
            //        EmployeeId = table.Column<int>(type: "integer", nullable: false),
            //        OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        RequiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        ShippedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        Freight = table.Column<int>(type: "integer", nullable: false),
            //        ShipName = table.Column<string>(type: "text", nullable: false),
            //        ShipAddress = table.Column<string>(type: "text", nullable: false),
            //        ShipCity = table.Column<string>(type: "text", nullable: false),
            //        ShipPostalCode = table.Column<string>(type: "text", nullable: false),
            //        ShipCountry = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Oders", x => x.PrderId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Products",
            //    columns: table => new
            //    {
            //        ProductId = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        ProductName = table.Column<string>(type: "text", nullable: false),
            //        SupplierId = table.Column<int>(type: "integer", nullable: false),
            //        CategoryId = table.Column<int>(type: "integer", nullable: false),
            //        QuantityPerUnit = table.Column<string>(type: "text", nullable: false),
            //        UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
            //        UnitsInStock = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Products", x => x.ProductId);
            //        table.CheckConstraint("CK_Product_UnitPrice", "UnitPrice >= 0");
            //        table.CheckConstraint("CK_UnitsInStock", "UnitsInStock >= 0");
            //        table.ForeignKey(
            //            name: "FK_Products_Categories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Categories",
            //            principalColumn: "CategoryId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderDetails",
            //    columns: table => new
            //    {
            //        OrderId = table.Column<int>(type: "integer", nullable: false),
            //        ProductId = table.Column<int>(type: "integer", nullable: false),
            //        UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
            //        Quantity = table.Column<int>(type: "integer", nullable: false),
            //        Discount = table.Column<decimal>(type: "numeric", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
            //        table.CheckConstraint("CK_Discount", "Discount >= 0 AND Discount <= 1");
            //        table.CheckConstraint("CK_Quantity", "Quantity > 0");
            //        table.CheckConstraint("CK_UnitPrice", "UnitPrice >= 0");
            //        table.ForeignKey(
            //            name: "FK_OrderDetails_Oders_OrderId",
            //            column: x => x.OrderId,
            //            principalTable: "Oders",
            //            principalColumn: "PrderId",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrderDetails_Products_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Products",
            //            principalColumn: "ProductId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderDetails_ProductId",
            //    table: "OrderDetails",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_CategoryId",
            //    table: "Products",
            //    column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Oders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
