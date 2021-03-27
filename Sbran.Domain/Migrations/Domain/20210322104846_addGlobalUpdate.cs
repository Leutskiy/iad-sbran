using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addGlobalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsularOffices",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    NameOfTheConsularPost = table.Column<string>(type: "text", nullable: true),
                    CountryOfLocation = table.Column<string>(type: "text", nullable: true),
                    CityOfLocation = table.Column<string>(type: "text", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsularOffices", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_ConsularOffices_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternationalAgreements",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    TheNameOfTheAgreement = table.Column<string>(type: "text", nullable: true),
                    TheFirstPartyToTheAgreement = table.Column<string>(type: "text", nullable: true),
                    TheSecondPartyToTheAgreement = table.Column<string>(type: "text", nullable: true),
                    PlaceOfSigning = table.Column<string>(type: "text", nullable: true),
                    DateOfEntry = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TextOfTheAgreement = table.Column<string>(type: "text", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalAgreements", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_InternationalAgreements_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    NameOfCompany = table.Column<string>(type: "text", nullable: true),
                    StatusInTheOrganization = table.Column<string>(type: "text", nullable: true),
                    DateOfEntry = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SiteOfTheOrganization = table.Column<string>(type: "text", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Memberships_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    ScientificAdvisor = table.Column<string>(type: "text", nullable: true),
                    TitleOfTheArticle = table.Column<string>(type: "text", nullable: true),
                    Abstract = table.Column<string>(type: "text", nullable: true),
                    Keywords = table.Column<string>(type: "text", nullable: true),
                    MainTextOfTheArticle = table.Column<string>(type: "text", nullable: true),
                    Literature = table.Column<string>(type: "text", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Publications_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScientificInterestss",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    ScientificAdvisor = table.Column<string>(type: "text", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificInterestss", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_ScientificInterestss_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsularOffices_EmployeeUid",
                schema: "domain",
                table: "ConsularOffices",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalAgreements_EmployeeUid",
                schema: "domain",
                table: "InternationalAgreements",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_EmployeeUid",
                schema: "domain",
                table: "Memberships",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_EmployeeUid",
                schema: "domain",
                table: "Publications",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificInterestss_EmployeeUid",
                schema: "domain",
                table: "ScientificInterestss",
                column: "EmployeeUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsularOffices",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "InternationalAgreements",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Memberships",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Publications",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ScientificInterestss",
                schema: "domain");
        }
    }
}
