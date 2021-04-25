using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class InitialDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.CreateTable(
                name: "Aliens",
                schema: "domain",
                columns: table => new
                {
                    AlienUid = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactUid = table.Column<Guid>(type: "uuid", nullable: false),
                    PassportUid = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    StateRegistrationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: true),
                    WorkPlace = table.Column<string>(type: "text", nullable: true),
                    WorkAddress = table.Column<string>(type: "text", nullable: true),
                    StayAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aliens", x => x.AlienUid);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    HomePhoneNumber = table.Column<string>(type: "text", nullable: true),
                    WorkPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    MobilePhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DocumentType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerUid = table.Column<Guid>(type: "uuid", nullable: true),
                    InvitationUid = table.Column<Guid>(type: "uuid", nullable: true),
                    ContactUid = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportUid = table.Column<Guid>(type: "uuid", nullable: true),
                    OrganizationUid = table.Column<Guid>(type: "uuid", nullable: true),
                    StateRegistrationUid = table.Column<Guid>(type: "uuid", nullable: true),
                    AcademicDegree = table.Column<string>(type: "text", nullable: true),
                    AcademicRank = table.Column<string>(type: "text", nullable: true),
                    Education = table.Column<string>(type: "text", nullable: true),
                    WorkPlace = table.Column<string>(type: "text", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerUid",
                        column: x => x.ManagerUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    AlienUid = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitDetailUid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    StateRegistrationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    LegalAddress = table.Column<string>(type: "text", nullable: true),
                    ScientificActivity = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    NameRus = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true),
                    SurnameRus = table.Column<string>(type: "text", nullable: true),
                    SurnameEng = table.Column<string>(type: "text", nullable: true),
                    PatronymicNameRus = table.Column<string>(type: "text", nullable: true),
                    PatronymicNameEng = table.Column<string>(type: "text", nullable: true),
                    BirthPlace = table.Column<string>(type: "text", nullable: true),
                    BirthCountry = table.Column<string>(type: "text", nullable: true),
                    Citizenship = table.Column<string>(type: "text", nullable: true),
                    Residence = table.Column<string>(type: "text", nullable: true),
                    ResidenceCountry = table.Column<string>(type: "text", nullable: true),
                    ResidenceRegion = table.Column<string>(type: "text", nullable: true),
                    IdentityDocument = table.Column<string>(type: "text", nullable: true),
                    IssuePlace = table.Column<string>(type: "text", nullable: true),
                    DepartmentCode = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "StateRegistrations",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    INN = table.Column<string>(type: "text", nullable: true),
                    OGRNIP = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateRegistrations", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "VisitDetails",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Goal = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    VisitingPoints = table.Column<string>(type: "text", nullable: true),
                    PeriodDays = table.Column<long>(type: "bigint", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VisaType = table.Column<string>(type: "text", nullable: true),
                    VisaCity = table.Column<string>(type: "text", nullable: true),
                    VisaCountry = table.Column<string>(type: "text", nullable: true),
                    VisaMultiplicity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDetails", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "ForeignParticipants",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    AlienUid = table.Column<Guid>(type: "uuid", nullable: false),
                    PassportUid = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignParticipants", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_ForeignParticipants_Invitations_InvitationUid",
                        column: x => x.InvitationUid,
                        principalSchema: "domain",
                        principalTable: "Invitations",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerUid",
                schema: "domain",
                table: "Employees",
                column: "ManagerUid");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignParticipants_InvitationUid",
                schema: "domain",
                table: "ForeignParticipants",
                column: "InvitationUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aliens",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ForeignParticipants",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Passports",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "StateRegistrations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "VisitDetails",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "domain");
        }
    }
}
