using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class updateVisitDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinancialCondition",
                schema: "domain",
                table: "VisitDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeReception",
                schema: "domain",
                table: "VisitDetails",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinancialCondition",
                schema: "domain",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "TypeReception",
                schema: "domain",
                table: "VisitDetails");
        }
    }
}
