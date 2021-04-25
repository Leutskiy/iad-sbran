using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addGlobalUpdateTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                schema: "domain",
                table: "Publications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                schema: "domain",
                table: "Memberships",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_EmployeeId1",
                schema: "domain",
                table: "Publications",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_EmployeeId1",
                schema: "domain",
                table: "Memberships",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Employees_EmployeeId1",
                schema: "domain",
                table: "Memberships",
                column: "EmployeeId1",
                principalSchema: "domain",
                principalTable: "Employees",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Employees_EmployeeId1",
                schema: "domain",
                table: "Publications",
                column: "EmployeeId1",
                principalSchema: "domain",
                principalTable: "Employees",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Employees_EmployeeId1",
                schema: "domain",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Employees_EmployeeId1",
                schema: "domain",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_EmployeeId1",
                schema: "domain",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_EmployeeId1",
                schema: "domain",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                schema: "domain",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                schema: "domain",
                table: "Memberships");
        }
    }
}
