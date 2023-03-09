using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cleaner.Migrations
{
    public partial class baseentitiesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratableBodyPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HolderTypeEnumId = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Battery = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    BodyPartTypeEnumId = table.Column<int>(type: "integer", nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratableBodyPart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneratableCleaner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HolderSeed = table.Column<string>(type: "text", nullable: false),
                    DefaultHealth = table.Column<int>(type: "integer", nullable: false),
                    DefaultBattery = table.Column<int>(type: "integer", nullable: false),
                    Rarity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratableCleaner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerBodyPart",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GeneratableBodyPartId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBodyPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerBodyPart_GeneratableBodyPart_GeneratableBodyPartId",
                        column: x => x.GeneratableBodyPartId,
                        principalTable: "GeneratableBodyPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBodyPart_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCleaner",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GeneratableCleanerId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCleaner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCleaner_GeneratableCleaner_GeneratableCleanerId",
                        column: x => x.GeneratableCleanerId,
                        principalTable: "GeneratableCleaner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCleaner_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerWarMachine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerCleanerId = table.Column<long>(type: "bigint", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerWarMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerWarMachine_PlayerCleaner_PlayerCleanerId",
                        column: x => x.PlayerCleanerId,
                        principalTable: "PlayerCleaner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerWarMachinePart",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerBodyPartId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerWarMachineId = table.Column<long>(type: "bigint", nullable: false),
                    HolderId = table.Column<char>(type: "character(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerWarMachinePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerWarMachinePart_PlayerBodyPart_PlayerBodyPartId",
                        column: x => x.PlayerBodyPartId,
                        principalTable: "PlayerBodyPart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerWarMachinePart_PlayerWarMachine_PlayerWarMachineId",
                        column: x => x.PlayerWarMachineId,
                        principalTable: "PlayerWarMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FirstLogInDate", "LastSeen" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 10, 2, 17, 24, 446, DateTimeKind.Unspecified).AddTicks(3534), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 10, 2, 17, 24, 446, DateTimeKind.Unspecified).AddTicks(3577), new TimeSpan(0, 3, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBodyPart_GeneratableBodyPartId",
                table: "PlayerBodyPart",
                column: "GeneratableBodyPartId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBodyPart_PlayerId",
                table: "PlayerBodyPart",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCleaner_GeneratableCleanerId",
                table: "PlayerCleaner",
                column: "GeneratableCleanerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCleaner_PlayerId",
                table: "PlayerCleaner",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWarMachine_PlayerCleanerId",
                table: "PlayerWarMachine",
                column: "PlayerCleanerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWarMachinePart_PlayerBodyPartId",
                table: "PlayerWarMachinePart",
                column: "PlayerBodyPartId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerWarMachinePart_PlayerWarMachineId",
                table: "PlayerWarMachinePart",
                column: "PlayerWarMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerWarMachinePart");

            migrationBuilder.DropTable(
                name: "PlayerBodyPart");

            migrationBuilder.DropTable(
                name: "PlayerWarMachine");

            migrationBuilder.DropTable(
                name: "GeneratableBodyPart");

            migrationBuilder.DropTable(
                name: "PlayerCleaner");

            migrationBuilder.DropTable(
                name: "GeneratableCleaner");

            migrationBuilder.UpdateData(
                table: "Player",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FirstLogInDate", "LastSeen" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 3, 10, 2, 14, 4, 554, DateTimeKind.Unspecified).AddTicks(237), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 3, 10, 2, 14, 4, 554, DateTimeKind.Unspecified).AddTicks(274), new TimeSpan(0, 3, 0, 0, 0)) });
        }
    }
}
