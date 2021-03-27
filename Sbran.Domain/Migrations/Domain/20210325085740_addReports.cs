using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                schema: "domain",
                table: "Invitations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureStatus",
                schema: "domain",
                table: "Departures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReportUid",
                schema: "domain",
                table: "Departures",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    MainPart = table.Column<string>(type: "text", nullable: true),
                    Findings = table.Column<string>(type: "text", nullable: true),
                    Suggestion = table.Column<string>(type: "text", nullable: true),
                    ForeignInterest = table.Column<string>(type: "text", nullable: true),
                    ReportType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Appendixs",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FileBinary = table.Column<byte[]>(type: "bytea", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    DepartureReportUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appendixs", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Appendixs_Reports_DepartureReportUid",
                        column: x => x.DepartureReportUid,
                        principalSchema: "domain",
                        principalTable: "Reports",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ReportId",
                schema: "domain",
                table: "Invitations",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_ReportUid",
                schema: "domain",
                table: "Departures",
                column: "ReportUid");

            migrationBuilder.CreateIndex(
                name: "IX_Appendixs_DepartureReportUid",
                schema: "domain",
                table: "Appendixs",
                column: "DepartureReportUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Reports_ReportUid",
                schema: "domain",
                table: "Departures",
                column: "ReportUid",
                principalSchema: "domain",
                principalTable: "Reports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Reports_ReportId",
                schema: "domain",
                table: "Invitations",
                column: "ReportId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Reports_ReportId",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropTable(
                name: "Appendixs",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "domain");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_ReportId",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Departures_ReportUid",
                schema: "domain",
                table: "Departures");

            migrationBuilder.DropColumn(
                name: "ReportId",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "DepartureStatus",
                schema: "domain",
                table: "Departures");

            migrationBuilder.DropColumn(
                name: "ReportUid",
                schema: "domain",
                table: "Departures");
        }
    }
}
