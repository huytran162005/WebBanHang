using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBanHang.Migrations
{
    public partial class SeedDataToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 1, 1, "Điện thoại" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 2, 2, "Máy tính bảng" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 3, 3, "Laptop" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, null, "images/products/a03793ae-fe6c-4a0f-baf7-74f4cc36d847.jpg", "Iphone 7", 300.0 },
                    { 2, 1, null, "images/products/a03793ae-fe6c-4a0f-baf7-74f4cc36d847.jpg", "Iphone 7s", 350.0 },
                    { 3, 1, null, "images/products/70e9a45f-7e3e-4233-845a-16d38b0851d2.jpg", "Iphone 8", 400.0 },
                    { 4, 1, null, "images/products/70e9a45f-7e3e-4233-845a-16d38b0851d2.jpg", "Iphone 8s", 420.0 },
                    { 5, 1, null, "images/products/c78e0ab4-5607-4376-9da3-3fc2434e5250.jpg", "Iphone 12", 630.0 },
                    { 6, 1, null, "images/products/c78e0ab4-5607-4376-9da3-3fc2434e5250.jpg", "Iphone 12 Pro", 750.0 },
                    { 7, 1, null, "images/products/f4d37377-2c34-492f-9369-fbcf4abbc4f2.jpg", "Iphone 14", 820.0 },
                    { 8, 1, null, "images/products/f4d37377-2c34-492f-9369-fbcf4abbc4f2.jpg", "Iphone 14 Pro", 950.0 },
                    { 9, 1, null, "images/products/de06bc90-f1a9-4cc6-a00d-9f8edb3d7ae0.jpg", "Iphone 15", 1200.0 },
                    { 10, 1, null, "images/products/56fb13eb-7b70-4eab-a7a0-092b15df334e.jpg", "Iphone 15 Pro Max ", 1450.0 },
                    { 11, 2, null, "images/products/2a7af498-e191-4c20-8597-2d2515208d87.jpg", "Ipad Gen 10", 750.0 },
                    { 13, 1, null, "images/products/bfa385ef-2c64-42a2-9aa1-71d0e2dfaa8c.webp", "OPPO FIND N5", 420.0},
                    { 15, 1, null, "images/products/20e532ba-0c00-4332-a36d-b3f1aa57f4bf.webp", "Nothing Phone 2A Plus", 750 },
                    { 14, 1, null, "images/products/330e4556-a2e4-4511-9f82-c189492128a8.webp", "Samsung Galaxy M55 5G", 550},
                    { 16, 1, null, "images/products/073d1ddc-7635-4ea0-947c-70cbfe58dab8.webp", "iPhone 16 Plus", 850},
                    { 17, 1, null, "images/products/77806a4a-979b-4e04-8dd1-a75de05c9597.webp", "Xiaomi 14T Pro", 899},

                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
