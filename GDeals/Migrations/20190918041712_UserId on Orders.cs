using Microsoft.EntityFrameworkCore.Migrations;

namespace GDeals.Web.Migrations
{
    public partial class UserIdonOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Orders");
        }
    }
}
