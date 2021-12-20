using Microsoft.EntityFrameworkCore.Migrations;

namespace Receipts_Server.Migrations
{
    public partial class change_relation_service_with_tariff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Services_ServiceId",
                table: "Tariffs");

            migrationBuilder.DropIndex(
                name: "IX_Tariffs_ServiceId",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Tariffs");

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_TariffId",
                table: "Services",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Tariffs_TariffId",
                table: "Services",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "TariffId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Tariffs_TariffId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_TariffId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Tariffs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_ServiceId",
                table: "Tariffs",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Services_ServiceId",
                table: "Tariffs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
