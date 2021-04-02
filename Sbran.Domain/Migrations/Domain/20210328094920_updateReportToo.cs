using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class updateReportToo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicDegree",
                schema: "domain",
                table: "ListOfScientists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Type",
                schema: "domain",
                table: "ListOfScientists",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicDegree",
                schema: "domain",
                table: "ListOfScientists");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "domain",
                table: "ListOfScientists");
        }
    }
}
