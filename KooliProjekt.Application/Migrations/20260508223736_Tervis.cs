using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Application.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class Tervis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kasutajad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parool = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasutajad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toiduained",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimetus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Energia = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Valgud = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Susivesikud = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    MillestSuhkrud = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Rasvad = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    MillestKullastunud = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Kiudained = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Sool = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toiduained", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patsiendid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isikukood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Synniaeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KasutajaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patsiendid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patsiendid_Kasutajad_KasutajaId",
                        column: x => x.KasutajaId,
                        principalTable: "Kasutajad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaaluMootmised",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kuupaev = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kaal = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    PatsientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaaluMootmised", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaaluMootmised_Patsiendid_PatsientId",
                        column: x => x.PatsientId,
                        principalTable: "Patsiendid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Soogikorrad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kuupaev = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tyyp = table.Column<int>(type: "int", nullable: false),
                    PatsientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soogikorrad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soogikorrad_Patsiendid_PatsientId",
                        column: x => x.PatsientId,
                        principalTable: "Patsiendid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VererohuMootmised",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kuupaev = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kellaaeg = table.Column<TimeSpan>(type: "time", nullable: false),
                    Sustoolne = table.Column<int>(type: "int", nullable: false),
                    Diastoolne = table.Column<int>(type: "int", nullable: false),
                    Pulss = table.Column<int>(type: "int", nullable: false),
                    PatsientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VererohuMootmised", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VererohuMootmised_Patsiendid_PatsientId",
                        column: x => x.PatsientId,
                        principalTable: "Patsiendid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeresuhkruMootmised",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kuupaev = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kellaaeg = table.Column<TimeSpan>(type: "time", nullable: false),
                    Veresuhkur = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    PatsientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeresuhkruMootmised", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeresuhkruMootmised_Patsiendid_PatsientId",
                        column: x => x.PatsientId,
                        principalTable: "Patsiendid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoogikorraRead",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kogus = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    SoogikordId = table.Column<int>(type: "int", nullable: false),
                    ToiduaineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoogikorraRead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoogikorraRead_Soogikorrad_SoogikordId",
                        column: x => x.SoogikordId,
                        principalTable: "Soogikorrad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoogikorraRead_Toiduained_ToiduaineId",
                        column: x => x.ToiduaineId,
                        principalTable: "Toiduained",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KaaluMootmised_PatsientId",
                table: "KaaluMootmised",
                column: "PatsientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patsiendid_KasutajaId",
                table: "Patsiendid",
                column: "KasutajaId");

            migrationBuilder.CreateIndex(
                name: "IX_Soogikorrad_PatsientId",
                table: "Soogikorrad",
                column: "PatsientId");

            migrationBuilder.CreateIndex(
                name: "IX_SoogikorraRead_SoogikordId",
                table: "SoogikorraRead",
                column: "SoogikordId");

            migrationBuilder.CreateIndex(
                name: "IX_SoogikorraRead_ToiduaineId",
                table: "SoogikorraRead",
                column: "ToiduaineId");

            migrationBuilder.CreateIndex(
                name: "IX_VererohuMootmised_PatsientId",
                table: "VererohuMootmised",
                column: "PatsientId");

            migrationBuilder.CreateIndex(
                name: "IX_VeresuhkruMootmised_PatsientId",
                table: "VeresuhkruMootmised",
                column: "PatsientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KaaluMootmised");

            migrationBuilder.DropTable(
                name: "SoogikorraRead");

            migrationBuilder.DropTable(
                name: "VererohuMootmised");

            migrationBuilder.DropTable(
                name: "VeresuhkruMootmised");

            migrationBuilder.DropTable(
                name: "Soogikorrad");

            migrationBuilder.DropTable(
                name: "Toiduained");

            migrationBuilder.DropTable(
                name: "Patsiendid");

            migrationBuilder.DropTable(
                name: "Kasutajad");
        }
    }
}
