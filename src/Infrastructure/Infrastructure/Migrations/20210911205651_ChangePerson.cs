using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Persons_RecomendatorIdentityNumber1",
                table: "Persons");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Persons_RecomendatorIdentityNumber",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "RecomendatorIdentityNumber1",
                table: "Persons",
                newName: "RecomendatorRecommendatorIdentityNumber");

            migrationBuilder.RenameColumn(
                name: "RecomendatorIdentityNumber",
                table: "Persons",
                newName: "RecommendatorIdentityNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RecomendatorIdentityNumber1",
                table: "Persons",
                newName: "IX_Persons_RecomendatorRecommendatorIdentityNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RecomendatorIdentityNumber",
                table: "Persons",
                newName: "IX_Persons_RecommendatorIdentityNumber");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Persons_RecommendatorIdentityNumber",
                table: "Persons",
                column: "RecommendatorIdentityNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Persons_RecomendatorRecommendatorIdentityNumber",
                table: "Persons",
                column: "RecomendatorRecommendatorIdentityNumber",
                principalTable: "Persons",
                principalColumn: "RecommendatorIdentityNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Persons_RecomendatorRecommendatorIdentityNumber",
                table: "Persons");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Persons_RecommendatorIdentityNumber",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "RecommendatorIdentityNumber",
                table: "Persons",
                newName: "RecomendatorIdentityNumber");

            migrationBuilder.RenameColumn(
                name: "RecomendatorRecommendatorIdentityNumber",
                table: "Persons",
                newName: "RecomendatorIdentityNumber1");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RecommendatorIdentityNumber",
                table: "Persons",
                newName: "IX_Persons_RecomendatorIdentityNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_RecomendatorRecommendatorIdentityNumber",
                table: "Persons",
                newName: "IX_Persons_RecomendatorIdentityNumber1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Persons_RecomendatorIdentityNumber",
                table: "Persons",
                column: "RecomendatorIdentityNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Persons_RecomendatorIdentityNumber1",
                table: "Persons",
                column: "RecomendatorIdentityNumber1",
                principalTable: "Persons",
                principalColumn: "RecomendatorIdentityNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
