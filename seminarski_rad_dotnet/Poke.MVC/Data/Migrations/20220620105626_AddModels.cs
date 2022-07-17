using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poke.MVC.Data.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokeBundles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeBundles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokeProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    InBundle = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokeCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_PokeCategories_PokeCategoryId",
                        column: x => x.PokeCategoryId,
                        principalTable: "PokeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokeBundleProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokeProductId = table.Column<int>(type: "int", nullable: false),
                    PokeBundleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeBundleProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokeBundleProducts_PokeBundles_PokeBundleId",
                        column: x => x.PokeBundleId,
                        principalTable: "PokeBundles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokeBundleProducts_PokeProducts_PokeProductId",
                        column: x => x.PokeProductId,
                        principalTable: "PokeProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokeProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokeProductId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokeProductCategories_PokeProducts_PokeProductId",
                        column: x => x.PokeProductId,
                        principalTable: "PokeProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokeProductCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokeBundleProducts_PokeBundleId",
                table: "PokeBundleProducts",
                column: "PokeBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_PokeBundleProducts_PokeProductId",
                table: "PokeBundleProducts",
                column: "PokeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PokeProductCategories_PokeProductId",
                table: "PokeProductCategories",
                column: "PokeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PokeProductCategories_SubCategoryId",
                table: "PokeProductCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_PokeCategoryId",
                table: "SubCategories",
                column: "PokeCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokeBundleProducts");

            migrationBuilder.DropTable(
                name: "PokeProductCategories");

            migrationBuilder.DropTable(
                name: "PokeBundles");

            migrationBuilder.DropTable(
                name: "PokeProducts");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "PokeCategories");
        }
    }
}
