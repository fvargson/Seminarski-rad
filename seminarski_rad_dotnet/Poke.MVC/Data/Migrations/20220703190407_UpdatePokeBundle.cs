using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poke.MVC.Data.Migrations
{
    public partial class UpdatePokeBundle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PokeBundles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PokeBundles");
        }
    }
}
