using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proiect1.Migrations
{
    public partial class recipe3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Recipe",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipe");
        }
    }
}
