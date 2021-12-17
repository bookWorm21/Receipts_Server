using Microsoft.EntityFrameworkCore.Migrations;

namespace Receipts_Server.Migrations
{
    public partial class apartment_field_in_property_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentNumber",
                table: "Properties",
                type: "integer",
                maxLength: 30,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Properties");
        }
    }
}
