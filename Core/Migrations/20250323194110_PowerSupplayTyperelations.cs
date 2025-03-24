using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class PowerSupplayTyperelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bataries_Workers_WorkerId",
                table: "Bataries");

            migrationBuilder.DropForeignKey(
                name: "FK_HandTools_Workers_WorkerId",
                table: "HandTools");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTools_Workers_WorkerId",
                table: "PowerTools");

            migrationBuilder.DropIndex(
                name: "IX_PowerTools_WorkerId",
                table: "PowerTools");

            migrationBuilder.DropIndex(
                name: "IX_HandTools_WorkerId",
                table: "HandTools");

            migrationBuilder.DropIndex(
                name: "IX_Bataries_WorkerId",
                table: "Bataries");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "PowerTools");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "HandTools");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Bataries");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_LastWorkerId",
                table: "PowerTools",
                column: "LastWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_HandTools_LastWorkerId",
                table: "HandTools",
                column: "LastWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bataries_LastWorkerId",
                table: "Bataries",
                column: "LastWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bataries_Workers_LastWorkerId",
                table: "Bataries",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HandTools_Workers_LastWorkerId",
                table: "HandTools",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTools_Workers_LastWorkerId",
                table: "PowerTools",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bataries_Workers_LastWorkerId",
                table: "Bataries");

            migrationBuilder.DropForeignKey(
                name: "FK_HandTools_Workers_LastWorkerId",
                table: "HandTools");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerTools_Workers_LastWorkerId",
                table: "PowerTools");

            migrationBuilder.DropIndex(
                name: "IX_PowerTools_LastWorkerId",
                table: "PowerTools");

            migrationBuilder.DropIndex(
                name: "IX_HandTools_LastWorkerId",
                table: "HandTools");

            migrationBuilder.DropIndex(
                name: "IX_Bataries_LastWorkerId",
                table: "Bataries");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "PowerTools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "HandTools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Bataries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_WorkerId",
                table: "PowerTools",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_HandTools_WorkerId",
                table: "HandTools",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bataries_WorkerId",
                table: "Bataries",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bataries_Workers_WorkerId",
                table: "Bataries",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HandTools_Workers_WorkerId",
                table: "HandTools",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTools_Workers_WorkerId",
                table: "PowerTools",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}
