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
                name: "LocaleCodes",
                schema: "dbo",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleCodes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "DeCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_DeCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_EnCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EsCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_EsCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FrCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_FrCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JaCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JaCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_JaCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PtBrCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtBrCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_PtBrCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RuCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_RuCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZhCnCities",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentName = table.Column<string>(maxLength: 64, nullable: true),
                    CountryName = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    CityName = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZhCnCities", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_ZhCnCities_LocaleCodes_LocaleCodeName",
                        column: x => x.LocaleCodeName,
                        principalSchema: "dbo",
                        principalTable: "LocaleCodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityLocations",
                schema: "dbo",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentCode = table.Column<string>(maxLength: 8, nullable: false),
                    CountryIsoCode = table.Column<string>(maxLength: 8, nullable: false),
                    Subdivision1IsoCode = table.Column<string>(maxLength: 4, nullable: true),
                    Subdivision2IsoCode = table.Column<string>(maxLength: 4, nullable: true),
                    MetroCode = table.Column<string>(maxLength: 4, nullable: true),
                    TimeZone = table.Column<string>(maxLength: 128, nullable: false),
                    IsInEuropeanUnion = table.Column<bool>(nullable: false),
                    RuCityGeonameId = table.Column<int>(nullable: true),
                    EnCityGeonameId = table.Column<int>(nullable: true),
                    DeCityGeonameId = table.Column<int>(nullable: true),
                    FrCityGeonameId = table.Column<int>(nullable: true),
                    EsCityGeonameId = table.Column<int>(nullable: true),
                    JaCityGeonameId = table.Column<int>(nullable: true),
                    PtBrCityGeonameId = table.Column<int>(nullable: true),
                    ZhCnCityGeonameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLocations", x => x.GeonameId);
                    table.ForeignKey(
                        name: "FK_CityLocations_DeCities_DeCityGeonameId",
                        column: x => x.DeCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "DeCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_EnCities_EnCityGeonameId",
                        column: x => x.EnCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "EnCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_EsCities_EsCityGeonameId",
                        column: x => x.EsCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "EsCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_FrCities_FrCityGeonameId",
                        column: x => x.FrCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "FrCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_JaCities_JaCityGeonameId",
                        column: x => x.JaCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "JaCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_PtBrCities_PtBrCityGeonameId",
                        column: x => x.PtBrCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "PtBrCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_RuCities_RuCityGeonameId",
                        column: x => x.RuCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "RuCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityLocations_ZhCnCities_ZhCnCityGeonameId",
                        column: x => x.ZhCnCityGeonameId,
                        principalSchema: "dbo",
                        principalTable: "ZhCnCities",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityBlocksIpv4s",
                schema: "dbo",
                columns: table => new
                {
                    Network = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    CityLocationGeonameId = table.Column<int>(nullable: true),
                    RegistredCountryGeonameId = table.Column<int>(nullable: true),
                    RepresentedCountryGeonameId = table.Column<int>(nullable: true),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 8, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    AccuracyRadius = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocksIpv4s", x => x.Network);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv4s_CityLocations_CityLocationGeonameId",
                        column: x => x.CityLocationGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv4s_CityLocations_RegistredCountryGeonameId",
                        column: x => x.RegistredCountryGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityBlocksIpv6s",
                schema: "dbo",
                columns: table => new
                {
                    Network = table.Column<string>(type: "varchar(47)", maxLength: 47, nullable: false),
                    CityLocationGeonameId = table.Column<int>(nullable: true),
                    RegistredCountryGeonameId = table.Column<int>(nullable: true),
                    CountryLocationGeonameId = table.Column<int>(nullable: true),
                    RepresentedCountryGeonameId = table.Column<int>(nullable: true),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 8, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    AccuracyRadius = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocksIpv6s", x => x.Network);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv6s_CityLocations_CityLocationGeonameId",
                        column: x => x.CityLocationGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityBlocksIpv6s_CityLocations_CountryLocationGeonameId",
                        column: x => x.CountryLocationGeonameId,
                        principalSchema: "dbo",
                        principalTable: "CityLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LocaleCodes",
                column: "Name",
                values: new object[]
                {
                    "en",
                    "de",
                    "fr",
                    "es",
                    "ru",
                    "ja",
                    "zh-CN",
                    "pt-BR"
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv4s_CityLocationGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv4s",
                column: "CityLocationGeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv4s_RegistredCountryGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv4s",
                column: "RegistredCountryGeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv6s_CityLocationGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv6s",
                column: "CityLocationGeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocksIpv6s_CountryLocationGeonameId",
                schema: "dbo",
                table: "CityBlocksIpv6s",
                column: "CountryLocationGeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_DeCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "DeCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_EnCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "EnCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_EsCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "EsCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_FrCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "FrCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_JaCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "JaCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_PtBrCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "PtBrCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_RuCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "RuCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_ZhCnCityGeonameId",
                schema: "dbo",
                table: "CityLocations",
                column: "ZhCnCityGeonameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeCities_LocaleCodeName",
                schema: "dbo",
                table: "DeCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_EnCities_LocaleCodeName",
                schema: "dbo",
                table: "EnCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_EsCities_LocaleCodeName",
                schema: "dbo",
                table: "EsCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_FrCities_LocaleCodeName",
                schema: "dbo",
                table: "FrCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_JaCities_LocaleCodeName",
                schema: "dbo",
                table: "JaCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_PtBrCities_LocaleCodeName",
                schema: "dbo",
                table: "PtBrCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_RuCities_LocaleCodeName",
                schema: "dbo",
                table: "RuCities",
                column: "LocaleCodeName");

            migrationBuilder.CreateIndex(
                name: "IX_ZhCnCities_LocaleCodeName",
                schema: "dbo",
                table: "ZhCnCities",
                column: "LocaleCodeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityBlocksIpv4s",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CityBlocksIpv6s",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CityLocations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DeCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EnCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EsCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FrCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "JaCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PtBrCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RuCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ZhCnCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LocaleCodes",
                schema: "dbo");
        }
    }
}
