using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addnewsandvotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "VoteLists",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    VoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoteId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteLists", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_VoteLists_Votes_VoteId",
                        column: x => x.VoteId,
                        principalSchema: "domain",
                        principalTable: "Votes",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteLists_Votes_VoteId1",
                        column: x => x.VoteId1,
                        principalSchema: "domain",
                        principalTable: "Votes",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoteLists_VoteId",
                schema: "domain",
                table: "VoteLists",
                column: "VoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoteLists_VoteId1",
                schema: "domain",
                table: "VoteLists",
                column: "VoteId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "VoteLists",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Votes",
                schema: "domain");
        }
    }
}
