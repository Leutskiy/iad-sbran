using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.System
{
    public partial class InitialDomainNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserUid",
                schema: "system",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserUid",
                schema: "system",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OrdinalNumber",
                schema: "system",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserUid",
                schema: "system",
                table: "Profiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileUid",
                schema: "system",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileUid",
                schema: "system",
                table: "Users",
                column: "ProfileUid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Profiles_ProfileUid",
                schema: "system",
                table: "Users",
                column: "ProfileUid",
                principalSchema: "system",
                principalTable: "Profiles",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Profiles_ProfileUid",
                schema: "system",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileUid",
                schema: "system",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileUid",
                schema: "system",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<long>(
                name: "OrdinalNumber",
                schema: "system",
                table: "Profiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUid",
                schema: "system",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserUid",
                schema: "system",
                table: "Profiles",
                column: "UserUid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserUid",
                schema: "system",
                table: "Profiles",
                column: "UserUid",
                principalSchema: "system",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
