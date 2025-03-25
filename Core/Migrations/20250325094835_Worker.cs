using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class Worker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    BryhadyrId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    BossId = table.Column<int>(type: "int", nullable: false),
                    Latitute = table.Column<double>(type: "float", nullable: true),
                    Longitute = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Bosses_BossId",
                        column: x => x.BossId,
                        principalTable: "Bosses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Workers_BryhadyrId",
                        column: x => x.BryhadyrId,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Workers_BossId",
                table: "Workers",
                column: "BossId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_BryhadyrId",
                table: "Workers",
                column: "BryhadyrId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_LocationId",
                table: "Workers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PositionId",
                table: "Workers",
                column: "PositionId");

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

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_PowerTools_LastWorkerId",
                table: "PowerTools");

            migrationBuilder.DropIndex(
                name: "IX_HandTools_LastWorkerId",
                table: "HandTools");

            migrationBuilder.DropIndex(
                name: "IX_Bataries_LastWorkerId",
                table: "Bataries");
        }
    }
}
