using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Receipts_Server.Migrations
{
    public partial class add_tariff_plan_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Services_ServiceId",
                table: "Tariffs");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Tariffs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "TariffPlans",
                columns: table => new
                {
                    TariffPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    TariffId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffPlans", x => x.TariffPlanId);
                    table.ForeignKey(
                        name: "FK_TariffPlans_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffPlans_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "TariffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TariffPlans_ServiceId",
                table: "TariffPlans",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffPlans_TariffId",
                table: "TariffPlans",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Services_ServiceId",
                table: "Tariffs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Services_ServiceId",
                table: "Tariffs");

            migrationBuilder.DropTable(
                name: "TariffPlans");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Tariffs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
