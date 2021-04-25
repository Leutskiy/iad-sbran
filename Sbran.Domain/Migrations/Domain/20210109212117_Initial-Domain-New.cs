using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class InitialDomainNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForeignParticipants_Invitations_InvitationUid",
                schema: "domain",
                table: "ForeignParticipants");

            migrationBuilder.DropColumn(
                name: "InvitationUid",
                schema: "domain",
                table: "VisitDetails");

            migrationBuilder.DropColumn(
                name: "AlienUid",
                schema: "domain",
                table: "ForeignParticipants");

            migrationBuilder.DropColumn(
                name: "InvitationUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InvitationUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.RenameColumn(
                name: "InvitationUid",
                schema: "domain",
                table: "ForeignParticipants",
                newName: "InvitationId");

            migrationBuilder.RenameIndex(
                name: "IX_ForeignParticipants_InvitationUid",
                schema: "domain",
                table: "ForeignParticipants",
                newName: "IX_ForeignParticipants_InvitationId");

            migrationBuilder.AlterColumn<int>(
                name: "VisaMultiplicity",
                schema: "domain",
                table: "VisitDetails",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "PeriodDays",
                schema: "domain",
                table: "VisitDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                schema: "domain",
                table: "VisitDetails",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalDate",
                schema: "domain",
                table: "VisitDetails",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateRegistrationUid",
                schema: "domain",
                table: "Organizations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "VisitDetailUid",
                schema: "domain",
                table: "Invitations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PassportUid",
                schema: "domain",
                table: "ForeignParticipants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "InvitationId",
                schema: "domain",
                table: "ForeignParticipants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "domain",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                schema: "domain",
                table: "Documents",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StateRegistrationUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PassportUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_StateRegistrationUid",
                schema: "domain",
                table: "Organizations",
                column: "StateRegistrationUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AlienUid",
                schema: "domain",
                table: "Invitations",
                column: "AlienUid");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_EmployeeUid",
                schema: "domain",
                table: "Invitations",
                column: "EmployeeUid");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_VisitDetailUid",
                schema: "domain",
                table: "Invitations",
                column: "VisitDetailUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ForeignParticipants_PassportUid",
                schema: "domain",
                table: "ForeignParticipants",
                column: "PassportUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContactUid",
                schema: "domain",
                table: "Employees",
                column: "ContactUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrganizationUid",
                schema: "domain",
                table: "Employees",
                column: "OrganizationUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PassportUid",
                schema: "domain",
                table: "Employees",
                column: "PassportUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StateRegistrationUid",
                schema: "domain",
                table: "Employees",
                column: "StateRegistrationUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_InvitationUid",
                schema: "domain",
                table: "Documents",
                column: "InvitationUid");

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_ContactUid",
                schema: "domain",
                table: "Aliens",
                column: "ContactUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_OrganizationUid",
                schema: "domain",
                table: "Aliens",
                column: "OrganizationUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_PassportUid",
                schema: "domain",
                table: "Aliens",
                column: "PassportUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aliens_StateRegistrationUid",
                schema: "domain",
                table: "Aliens",
                column: "StateRegistrationUid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aliens_Contacts_ContactUid",
                schema: "domain",
                table: "Aliens",
                column: "ContactUid",
                principalSchema: "domain",
                principalTable: "Contacts",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aliens_Organizations_OrganizationUid",
                schema: "domain",
                table: "Aliens",
                column: "OrganizationUid",
                principalSchema: "domain",
                principalTable: "Organizations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aliens_Passports_PassportUid",
                schema: "domain",
                table: "Aliens",
                column: "PassportUid",
                principalSchema: "domain",
                principalTable: "Passports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aliens_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Aliens",
                column: "StateRegistrationUid",
                principalSchema: "domain",
                principalTable: "StateRegistrations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Invitations_InvitationUid",
                schema: "domain",
                table: "Documents",
                column: "InvitationUid",
                principalSchema: "domain",
                principalTable: "Invitations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Contacts_ContactUid",
                schema: "domain",
                table: "Employees",
                column: "ContactUid",
                principalSchema: "domain",
                principalTable: "Contacts",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Organizations_OrganizationUid",
                schema: "domain",
                table: "Employees",
                column: "OrganizationUid",
                principalSchema: "domain",
                principalTable: "Organizations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Passports_PassportUid",
                schema: "domain",
                table: "Employees",
                column: "PassportUid",
                principalSchema: "domain",
                principalTable: "Passports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Employees",
                column: "StateRegistrationUid",
                principalSchema: "domain",
                principalTable: "StateRegistrations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignParticipants_Invitations_InvitationId",
                schema: "domain",
                table: "ForeignParticipants",
                column: "InvitationId",
                principalSchema: "domain",
                principalTable: "Invitations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignParticipants_Passports_PassportUid",
                schema: "domain",
                table: "ForeignParticipants",
                column: "PassportUid",
                principalSchema: "domain",
                principalTable: "Passports",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Aliens_AlienUid",
                schema: "domain",
                table: "Invitations",
                column: "AlienUid",
                principalSchema: "domain",
                principalTable: "Aliens",
                principalColumn: "AlienUid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Employees_EmployeeUid",
                schema: "domain",
                table: "Invitations",
                column: "EmployeeUid",
                principalSchema: "domain",
                principalTable: "Employees",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_VisitDetails_VisitDetailUid",
                schema: "domain",
                table: "Invitations",
                column: "VisitDetailUid",
                principalSchema: "domain",
                principalTable: "VisitDetails",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Organizations",
                column: "StateRegistrationUid",
                principalSchema: "domain",
                principalTable: "StateRegistrations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aliens_Contacts_ContactUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropForeignKey(
                name: "FK_Aliens_Organizations_OrganizationUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropForeignKey(
                name: "FK_Aliens_Passports_PassportUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropForeignKey(
                name: "FK_Aliens_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Invitations_InvitationUid",
                schema: "domain",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Contacts_ContactUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Organizations_OrganizationUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Passports_PassportUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_ForeignParticipants_Invitations_InvitationId",
                schema: "domain",
                table: "ForeignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ForeignParticipants_Passports_PassportUid",
                schema: "domain",
                table: "ForeignParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Aliens_AlienUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Employees_EmployeeUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_VisitDetails_VisitDetailUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_StateRegistrations_StateRegistrationUid",
                schema: "domain",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_StateRegistrationUid",
                schema: "domain",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_AlienUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_EmployeeUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_VisitDetailUid",
                schema: "domain",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_ForeignParticipants_PassportUid",
                schema: "domain",
                table: "ForeignParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ContactUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OrganizationUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PassportUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StateRegistrationUid",
                schema: "domain",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Documents_InvitationUid",
                schema: "domain",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Aliens_ContactUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropIndex(
                name: "IX_Aliens_OrganizationUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropIndex(
                name: "IX_Aliens_PassportUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.DropIndex(
                name: "IX_Aliens_StateRegistrationUid",
                schema: "domain",
                table: "Aliens");

            migrationBuilder.RenameColumn(
                name: "InvitationId",
                schema: "domain",
                table: "ForeignParticipants",
                newName: "InvitationUid");

            migrationBuilder.RenameIndex(
                name: "IX_ForeignParticipants_InvitationId",
                schema: "domain",
                table: "ForeignParticipants",
                newName: "IX_ForeignParticipants_InvitationUid");

            migrationBuilder.AlterColumn<int>(
                name: "VisaMultiplicity",
                schema: "domain",
                table: "VisitDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PeriodDays",
                schema: "domain",
                table: "VisitDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                schema: "domain",
                table: "VisitDetails",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalDate",
                schema: "domain",
                table: "VisitDetails",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationUid",
                schema: "domain",
                table: "VisitDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StateRegistrationUid",
                schema: "domain",
                table: "Organizations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "VisitDetailUid",
                schema: "domain",
                table: "Invitations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PassportUid",
                schema: "domain",
                table: "ForeignParticipants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InvitationUid",
                schema: "domain",
                table: "ForeignParticipants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AlienUid",
                schema: "domain",
                table: "ForeignParticipants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationUid",
                schema: "domain",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "domain",
                table: "Documents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                schema: "domain",
                table: "Documents",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateRegistrationUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PassportUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContactUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationUid",
                schema: "domain",
                table: "Aliens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ForeignParticipants_Invitations_InvitationUid",
                schema: "domain",
                table: "ForeignParticipants",
                column: "InvitationUid",
                principalSchema: "domain",
                principalTable: "Invitations",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
