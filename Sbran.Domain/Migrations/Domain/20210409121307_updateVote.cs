using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class updateVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoteLists_VoteId",
                schema: "domain",
                table: "VoteLists");

            migrationBuilder.CreateIndex(
                name: "IX_VoteLists_VoteId",
                schema: "domain",
                table: "VoteLists",
                column: "VoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoteLists_VoteId",
                schema: "domain",
                table: "VoteLists");

            migrationBuilder.CreateIndex(
                name: "IX_VoteLists_VoteId",
                schema: "domain",
                table: "VoteLists",
                column: "VoteId",
                unique: true);
        }
    }
}
