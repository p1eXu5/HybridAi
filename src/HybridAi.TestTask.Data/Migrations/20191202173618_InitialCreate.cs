using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HybridAi.TestTask.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "CityLocations",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocaleCode = table.Column<string>(maxLength: 8, nullable: true),
                    ContinentCode = table.Column<string>(maxLength: 8, nullable: true),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CopuntryIsoCode = table.Column<string>(maxLength: 8, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1IsoCode = table.Column<string>(maxLength: 4, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2IsoCode = table.Column<string>(maxLength: 4, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    MetroCode = table.Column<string>(maxLength: 4, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 128, nullable: true),
                    IsInEuropeanUnion = table.Column<bool>(nullable: false),
                    CityName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLocations", x => x.GeonameId);
                });

            migrationBuilder.CreateTable(
                name: "CityBlocksIpv4",
                schema: "dbo",
                columns: table => new
                {
                    Network = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    RegistredCountryGeonameId = table.Column<int>(nullable: false),
                    RepresentedCountryGeonameId = table.Column<int>(nullable: false),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 8, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AccuracyRadius = table.Column<short>(nullable: false),
                    CityLocationGeonameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocksIpv4", x => x.Network);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv4_CityLocations_CityLocationGeonameId",
                        column: x => x.CityLocationGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityBlocksIpv6",
                schema: "dbo",
                columns: table => new
                {
                    Network = table.Column<string>(type: "varchar(39)", maxLength: 39, nullable: false),
                    RegistredCountryGeonameId = table.Column<int>(nullable: false),
                    RepresentedCountryGeonameId = table.Column<int>(nullable: false),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 8, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AccuracyRadius = table.Column<short>(nullable: false),
                    CityLocationGeonameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocksIpv6", x => x.Network);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv6_CityLocations_CityLocationGeonameId",
                        column: x => x.CityLocationGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv4_CityLocationGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv4",
                column: "CityLocationGeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv6_CityLocationGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv6",
                column: "CityLocationGeonameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityBlocksIpv4",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CityBlocksIpv6",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CityLocations",
                schema: "dbo");
        }
    }
}
