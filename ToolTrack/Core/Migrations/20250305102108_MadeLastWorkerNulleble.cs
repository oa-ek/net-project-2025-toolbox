using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class MadeLastWorkerNulleble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "PowerTools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "HandTools",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "Bataries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "PowerTools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "HandTools",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastWorkerId",
                table: "Bataries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bataries_Workers_LastWorkerId",
                table: "Bataries",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HandTools_Workers_LastWorkerId",
                table: "HandTools",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PowerTools_Workers_LastWorkerId",
                table: "PowerTools",
                column: "LastWorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
