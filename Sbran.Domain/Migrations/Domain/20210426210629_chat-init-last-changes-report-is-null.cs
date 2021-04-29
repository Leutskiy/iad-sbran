using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class chatinitlastchangesreportisnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Reports_ReportUid",
                schema: "domain",
                table: "Departures");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Reports_ReportUid",
                schema: "domain",
                table: "Departures",
                column: "ReportUid",
                principalSchema: "domain",
                principalTable: "Reports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Reports_ReportUid",
                schema: "domain",
                table: "Departures");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Reports_ReportUid",
                schema: "domain",
                table: "Departures",
                column: "ReportUid",
                principalSchema: "domain",
                principalTable: "Reports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
