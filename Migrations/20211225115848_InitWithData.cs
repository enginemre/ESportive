using Microsoft.EntityFrameworkCore.Migrations;

namespace SportiveOrder.Migrations
{
    public partial class InitWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Antreman" },
                    { 2, "Futbol" },
                    { 3, "Günlük Stil" },
                    { 4, "Koşu" },
                    { 5, "Tenis" },
                    { 6, "Voleybol" },
                    { 7, "Diğer" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Color", "KDV", "ProductGroup", "ProductName", "PurchasePrice", "SalePrice", "Size", "Stock", "StockCode" },
                values: new object[,]
                {
                    { 4, "0887751123538", 1, "PEMBE", 18, "İP", "JORDAN SKILLS TEST3", 11m, 153m, "13", 91, "J.000.1884.041.03" },
                    { 2, "0887791158918", 2, "MVAİ", 18, "TOP", "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 01", 13m, 333m, "7", 21, "J.0050.41.04561.03" },
                    { 3, "0887761155048", 3, "KIRMIZI", 18, "SULUK", "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 02", 134m, 483m, "2", 41, "J.05690.546.041.03" },
                    { 5, "0887792559008", 3, "YEŞİL", 18, "ELDİVEN", "JORDAN SKILLS TEST1", 23m, 283m, "3", 51, "J.000.1884.041.03" },
                    { 1, "0887791159038", 5, "SİYAH", 18, "TOP", "JORDAN SKILLS BLACK/WOLF GREY/GYM RED/GYM RED 03", 123m, 863m, "56", 61, "J.00560.1884.0456.03" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 5);
        }
    }
}
