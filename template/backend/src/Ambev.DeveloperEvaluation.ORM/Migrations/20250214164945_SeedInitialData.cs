using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Email", "Password", "Phone", "Role", "Status", "CreatedAt", "Name_Firstname", "Name_Lastname", "Address_Street", "Address_Number", "Address_City", "Address_Zipcode", "Address_Geolocation_Latitude", "Address_Geolocation_Longitude" },
                values: new object[] { "admin", "admin@admin.com", "$2b$12$mtuGxI8yQXGvviLbhqTt3uN8jnYeNDVEgPbXJifLyWeNylptvfwzi", "1234567890", "Admin", "Active", DateTime.UtcNow, "Administrator", "System", "Example Street", 123, "Example City", "12345678", "12.345678", "-98.765432" }
            );

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Name" },
                values: new object[] { "Branch 1" }
            );

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Title", "Description", "Category", "Price", "Image", "RatingCount", "RatingRate" },
                values: new object[,]
                {
                    { "Urban Sneaker", "Comfortable casual sneaker, ideal for everyday use.", "Sneakers", 150m, "sneaker1.jpg", 12, 4.6m },
                    { "Elegance Derby", "Leather dress shoe, perfect for formal occasions.", "Dress Shoes", 350m, "derby.jpg", 8, 4.4m },
                    { "Extreme Runner", "High-performance running shoe with great cushioning.", "Sports Shoes", 280m, "running.jpg", 15, 4.7m },
                    { "Classic Loafer", "Suede loafer, comfortable and stylish.", "Loafers", 220m, "loafer.jpg", 9, 4.5m },
                    { "Adventure Boot", "Durable boot for trails and outdoor adventures.", "Boots", 400m, "boot.jpg", 14, 4.8m }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
            migrationBuilder.Sql("DELETE FROM Branches");
            migrationBuilder.Sql("DELETE FROM Users");

            migrationBuilder.Sql("SELECT SETVAL('\"Users_Id_seq\"', 1, false)");
            migrationBuilder.Sql("SELECT SETVAL('\"Branches_Id_seq\"', 1, false)");
            migrationBuilder.Sql("SELECT SETVAL('\"Products_Id_seq\"', 1, false)");
        }
    }
}
