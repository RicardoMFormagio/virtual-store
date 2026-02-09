using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Caderno Espiral pequeno', 9.55, 'Caderno Espiral pequeno 100 folhas universitário 1 UN', 60, 'escolar/caderno_espiral_100.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Lápis Coloridos', 3.45, 'Lápis Coloridos Faber Castel 12 unidades', 20, 'escolar/lapis_coloridos1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Caneta Corretiva', 5.33, 'Caneta Corretiva 0.8 mm Spiral PT 1 UN', 50, 'escolar/caneta_corretiva1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Caneta Esferográfica', 12.80, 'Caneta Esferográfica BIC Cristal Fashion, 10 Cores Vi...', 100, 'escolar/caneta_bic4.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Caneta Escreve e Apaga', 15.90, 'Caneta Escreve e Apaga Frixion 0,7mm Esferográfica...', 50, 'escolar/caneta_escreve_apaga1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Borracha Branca', 4.50, 'Borracha Branca Lavável pequena oval', 25, 'escolar/borracha_branca1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Caderno de Anotações 100 fl', 18.99, 'Caderno de Anotações 13x21 cm sem pauta preto co...', 35, 'escolar/caderno_anotacoes1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Cola Branca 1 Kg', 23.80, 'Cola Branca Cascorez 1 Kg', 25, 'escolar/colabranca1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Fichário Universitário', 23.55, 'Fichário Universitário 4 argolas com ziper 1 UN', 30, 'escolar/fichario_univ1.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Bloco de desenho', 10.50, 'Bloco de desenho A4 branco Carson 1UN', 15, 'escolar/191082.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Papel Sulfite A4 75 g', 11.20, 'Papel Sulfite Chamequinho A4 75 g com 4 cores', 25, 'escolar/papel_sulfite.jpg', 1)");

            mb.Sql("INSERT INTO Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                   "VALUES('Corretivo em fita', 17.25, 'Corretivo em fita micro rolly Pritt Henkel 1 PT', 40, 'escolar/corretivo_emfita1.jpg', 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products WHERE ImageURL LIKE 'escolar/%'");
        }
    }
}