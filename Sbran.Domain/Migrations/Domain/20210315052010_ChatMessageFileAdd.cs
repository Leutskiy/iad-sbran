using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class ChatMessageFileAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessageFiles",
                schema: "domain",
                columns: table => new
                {
                    ChatMessageFileUid = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatMessageUid = table.Column<Guid>(type: "uuid", nullable: false),
                    File = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageFiles", x => x.ChatMessageFileUid);
                    table.ForeignKey(
                        name: "FK_ChatMessageFiles_ChatMessages_ChatMessageUid",
                        column: x => x.ChatMessageUid,
                        principalSchema: "domain",
                        principalTable: "ChatMessages",
                        principalColumn: "MessagesUid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageFiles_ChatMessageUid",
                schema: "domain",
                table: "ChatMessageFiles",
                column: "ChatMessageUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessageFiles",
                schema: "domain");
        }
    }
}
