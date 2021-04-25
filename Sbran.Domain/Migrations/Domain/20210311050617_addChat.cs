using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                schema: "domain",
                columns: table => new
                {
                    ChatRoomUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.ChatRoomUid);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoomLists",
                schema: "domain",
                columns: table => new
                {
                    ChatRoomListUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomUid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomLists", x => x.ChatRoomListUid);
                    table.ForeignKey(
                        name: "FK_ChatRoomLists_ChatRooms_ChatRoomUid",
                        column: x => x.ChatRoomUid,
                        principalSchema: "domain",
                        principalTable: "ChatRooms",
                        principalColumn: "ChatRoomUid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messageses",
                schema: "domain",
                columns: table => new
                {
                    MessagesUid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messageses", x => x.MessagesUid);
                    table.ForeignKey(
                        name: "FK_Messageses_ChatRooms_ChatRoomUid",
                        column: x => x.ChatRoomUid,
                        principalSchema: "domain",
                        principalTable: "ChatRooms",
                        principalColumn: "ChatRoomUid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomLists_ChatRoomUid",
                schema: "domain",
                table: "ChatRoomLists",
                column: "ChatRoomUid");

            migrationBuilder.CreateIndex(
                name: "IX_Messageses_ChatRoomUid",
                schema: "domain",
                table: "Messageses",
                column: "ChatRoomUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoomLists",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Messageses",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ChatRooms",
                schema: "domain");

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    WebPages = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Account = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserUid",
                schema: "domain",
                table: "Employees",
                column: "UserUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ProfileId",
                table: "User",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_User_UserUid",
                schema: "domain",
                table: "Employees",
                column: "UserUid",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
