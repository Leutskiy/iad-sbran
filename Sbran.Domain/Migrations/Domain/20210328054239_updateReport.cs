using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class updateReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "domain",
                table: "Appendixs");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "domain",
                table: "Reports",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId1",
                schema: "domain",
                table: "Appendixs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListOfScientists",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    WorkPlace = table.Column<string>(type: "text", nullable: true),
                    FileBinary = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ReportUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfScientists", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_ListOfScientists_Reports_ReportId1",
                        column: x => x.ReportId1,
                        principalSchema: "domain",
                        principalTable: "Reports",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListOfScientists_Reports_ReportUid",
                        column: x => x.ReportUid,
                        principalSchema: "domain",
                        principalTable: "Reports",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appendixs_ReportId1",
                schema: "domain",
                table: "Appendixs",
                column: "ReportId1");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfScientists_ReportId1",
                schema: "domain",
                table: "ListOfScientists",
                column: "ReportId1");

            migrationBuilder.CreateIndex(
                name: "IX_ListOfScientists_ReportUid",
                schema: "domain",
                table: "ListOfScientists",
                column: "ReportUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Appendixs_Reports_ReportId1",
                schema: "domain",
                table: "Appendixs",
                column: "ReportId1",
                principalSchema: "domain",
                principalTable: "Reports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appendixs_Reports_ReportId1",
                schema: "domain",
                table: "Appendixs");

            migrationBuilder.DropTable(
                name: "ListOfScientists",
                schema: "domain");

            migrationBuilder.DropIndex(
                name: "IX_Appendixs_ReportId1",
                schema: "domain",
                table: "Appendixs");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "domain",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportId1",
                schema: "domain",
                table: "Appendixs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "domain",
                table: "Appendixs",
                type: "text",
                nullable: true);
        }
    }
}
