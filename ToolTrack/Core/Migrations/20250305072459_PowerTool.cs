using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class PowerTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    LastWorkerId = table.Column<int>(type: "int", nullable: false),
                    LastLocationId = table.Column<int>(type: "int", nullable: false),
                    ToolModelId = table.Column<int>(type: "int", nullable: false),
                    HaveCase = table.Column<bool>(type: "bit", nullable: false),
                    DateMade = table.Column<DateOnly>(type: "date", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PowerSupplyTypeId = table.Column<int>(type: "int", nullable: false),
                    ToolTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerTools_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerTools_PowerSupplyTypes_PowerSupplyTypeId",
                        column: x => x.PowerSupplyTypeId,
                        principalTable: "PowerSupplyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerTools_ToolModels_ToolModelId",
                        column: x => x.ToolModelId,
                        principalTable: "ToolModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerTools_ToolTypes_ToolTypeId",
                        column: x => x.ToolTypeId,
                        principalTable: "ToolTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PowerTools_Workers_LastWorkerId",
                        column: x => x.LastWorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_ConditionId",
                table: "PowerTools",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_LastWorkerId",
                table: "PowerTools",
                column: "LastWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_PowerSupplyTypeId",
                table: "PowerTools",
                column: "PowerSupplyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_ToolModelId",
                table: "PowerTools",
                column: "ToolModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerTools_ToolTypeId",
                table: "PowerTools",
                column: "ToolTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerTools");
        }
    }
}
