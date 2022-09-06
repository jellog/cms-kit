using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGap.CmsKit.Pro.Migrations
{
    public partial class AddedWidgetToPoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Widget",
                table: "CmsPolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CmsBlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Widget",
                table: "CmsPolls");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CmsBlogPosts");
        }
    }
}
