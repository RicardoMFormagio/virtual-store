using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)" +
                   "Values('Caderno', 7.55, 'Caderno', 10, 'cardeno1.jpg', 1)");
            
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "Values('Caneta Azul', 2.50, 'Caneta esferográfica azul', 100, 'caneta_azul.jpg', 1)");

            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "Values('Clips', 15.90, 'Clips de papel', 25, 'clips.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Products");
        }
    }
}
