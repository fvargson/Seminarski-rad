using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poke.MVC.Data.Migrations
{
    public partial class CategoryProductDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PokeCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Generation" },
                    { 2, "Type" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "PokeProducts",
                columns: new[] { "Id", "InBundle", "Name", "PokemonId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, false, "Bulbasaur", 1, 2500, 50 },
                    { 2, false, "Charmander", 4, 2500, 50 },
                    { 3, false, "Squirtle", 7, 2500, 50 },
                    { 4, false, "Chikorita", 152, 2500, 50 },
                    { 5, false, "Cyndaquil", 155, 2500, 50 },
                    { 6, false, "Totodile", 158, 2500, 50 },
                    { 7, false, "Treecko", 252, 2500, 50 },
                    { 8, false, "Torchic", 255, 2500, 50 },
                    { 9, false, "Mudkip", 258, 2500, 50 },
                    { 10, false, "Turtwig", 387, 2500, 50 },
                    { 11, false, "Chimchar", 390, 2500, 50 },
                    { 12, false, "Piplup", 393, 2500, 50 },
                    { 13, false, "Snivy", 495, 2500, 50 },
                    { 14, false, "Tepig", 498, 2500, 50 },
                    { 15, false, "Oshawott", 501, 2500, 50 },
                    { 16, false, "Chespin", 650, 2500, 50 },
                    { 17, false, "Fennekin", 653, 2500, 50 },
                    { 18, false, "Froakie", 656, 2500, 50 },
                    { 19, false, "Rowlet", 722, 2500, 50 },
                    { 20, false, "Litten", 725, 2500, 50 },
                    { 21, false, "Popplio", 728, 2500, 50 },
                    { 22, false, "Grookey", 810, 2500, 50 },
                    { 23, false, "Scorbunny", 813, 2500, 50 },
                    { 24, false, "Sobble", 816, 2500, 50 }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Name", "PokeCategoryId" },
                values: new object[,]
                {
                    { 1, "Gen. 1", 1 },
                    { 2, "Gen. 2", 1 },
                    { 3, "Gen. 3", 1 },
                    { 4, "Gen. 4", 1 },
                    { 5, "Gen. 5", 1 },
                    { 6, "Gen. 6", 1 },
                    { 7, "Gen. 7", 1 },
                    { 8, "Gen. 8", 1 },
                    { 9, "normal", 2 },
                    { 10, "fighting", 2 },
                    { 11, "flying", 2 },
                    { 12, "poison", 2 },
                    { 13, "ground", 2 },
                    { 14, "rock", 2 },
                    { 15, "bug", 2 },
                    { 16, "ghost", 2 },
                    { 17, "steel", 2 },
                    { 18, "fire", 2 },
                    { 19, "water", 2 },
                    { 20, "grass", 2 },
                    { 21, "electric", 2 },
                    { 22, "psychic", 2 },
                    { 23, "ice", 2 },
                    { 24, "dragon", 2 },
                    { 25, "dark", 2 },
                    { 26, "fairy", 2 },
                    { 27, "Starter", 3 },
                    { 28, "Legendary", 3 },
                    { 29, "Mythical", 3 },
                    { 30, "Baby", 3 }
                });

            migrationBuilder.InsertData(
                table: "PokeProductCategories",
                columns: new[] { "Id", "PokeProductId", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 20 },
                    { 3, 1, 12 },
                    { 4, 1, 27 },
                    { 5, 2, 1 },
                    { 6, 2, 18 },
                    { 7, 2, 27 },
                    { 8, 3, 1 },
                    { 9, 3, 19 },
                    { 10, 3, 27 },
                    { 11, 4, 2 },
                    { 12, 4, 20 },
                    { 13, 4, 27 },
                    { 14, 5, 2 },
                    { 15, 5, 18 },
                    { 16, 5, 27 },
                    { 17, 6, 2 },
                    { 18, 6, 19 },
                    { 19, 6, 27 },
                    { 20, 7, 3 },
                    { 21, 7, 20 },
                    { 22, 7, 27 },
                    { 23, 8, 3 },
                    { 24, 8, 18 },
                    { 25, 8, 27 },
                    { 26, 9, 3 },
                    { 27, 9, 19 },
                    { 28, 9, 27 },
                    { 29, 10, 4 },
                    { 30, 10, 20 },
                    { 31, 10, 27 },
                    { 32, 11, 4 },
                    { 33, 11, 18 },
                    { 34, 11, 27 },
                    { 35, 12, 4 },
                    { 36, 12, 19 },
                    { 37, 12, 27 },
                    { 38, 13, 5 },
                    { 39, 13, 20 },
                    { 40, 13, 27 },
                    { 41, 14, 5 },
                    { 42, 14, 18 }
                });

            migrationBuilder.InsertData(
                table: "PokeProductCategories",
                columns: new[] { "Id", "PokeProductId", "SubCategoryId" },
                values: new object[,]
                {
                    { 43, 14, 27 },
                    { 44, 15, 5 },
                    { 45, 15, 19 },
                    { 46, 15, 27 },
                    { 47, 16, 6 },
                    { 48, 16, 20 },
                    { 49, 16, 27 },
                    { 50, 17, 6 },
                    { 51, 17, 18 },
                    { 52, 17, 27 },
                    { 53, 18, 6 },
                    { 54, 18, 19 },
                    { 55, 18, 27 },
                    { 56, 19, 7 },
                    { 57, 19, 20 },
                    { 58, 19, 11 },
                    { 59, 19, 27 },
                    { 60, 20, 7 },
                    { 61, 20, 18 },
                    { 62, 20, 27 },
                    { 63, 21, 7 },
                    { 64, 21, 19 },
                    { 65, 21, 27 },
                    { 66, 22, 8 },
                    { 67, 22, 20 },
                    { 68, 22, 27 },
                    { 69, 23, 8 },
                    { 70, 23, 18 },
                    { 71, 23, 27 },
                    { 72, 24, 8 },
                    { 73, 24, 19 },
                    { 74, 24, 27 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "PokeProductCategories",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PokeProducts",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PokeCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PokeCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PokeCategories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
