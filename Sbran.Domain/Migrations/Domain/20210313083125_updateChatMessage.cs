using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class updateChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messageses",
                schema: "domain");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                schema: "domain",
                columns: table => new
                {
                    MessagesUid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.MessagesUid);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomUid",
                        column: x => x.ChatRoomUid,
                        principalSchema: "domain",
                        principalTable: "ChatRooms",
                        principalColumn: "ChatRoomUid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoomUid",
                schema: "domain",
                table: "ChatMessages",
                column: "ChatRoomUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages",
                schema: "domain");

            migrationBuilder.CreateTable(
                name: "Messageses",
                schema: "domain",
                columns: table => new
                {
                    MessagesUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomUid = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_Messageses_ChatRoomUid",
                schema: "domain",
                table: "Messageses",
                column: "ChatRoomUid");
        }
    }
}
