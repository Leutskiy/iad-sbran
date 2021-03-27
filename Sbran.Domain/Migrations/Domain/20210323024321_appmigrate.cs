using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class appmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                schema: "domain",
                table: "ConsularOffices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScientificInterestss_EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConsularOffices_EmployeeId1",
                schema: "domain",
                table: "ConsularOffices",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsularOffices_Employees_EmployeeId1",
                schema: "domain",
                table: "ConsularOffices",
                column: "EmployeeId1",
                principalSchema: "domain",
                principalTable: "Employees",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificInterestss_Employees_EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss",
                column: "EmployeeId1",
                principalSchema: "domain",
                principalTable: "Employees",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsularOffices_Employees_EmployeeId1",
                schema: "domain",
                table: "ConsularOffices");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificInterestss_Employees_EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss");

            migrationBuilder.DropIndex(
                name: "IX_ScientificInterestss_EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss");

            migrationBuilder.DropIndex(
                name: "IX_ConsularOffices_EmployeeId1",
                schema: "domain",
                table: "ConsularOffices");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                schema: "domain",
                table: "ScientificInterestss");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                schema: "domain",
                table: "ConsularOffices");
        }
    }
}
