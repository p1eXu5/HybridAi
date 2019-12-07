using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HybridAi.TestTask.Data.Migrations
{
    public partial class LocaleCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "ContinentName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "CountryName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "LocaleCode",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "Subdivision1Name",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "Subdivision2Name",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.AddColumn<string>(
                name: "EnCityCityName",
                schema: "dbo",
                table: "CityLocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnCityContinentName",
                schema: "dbo",
                table: "CityLocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnCityCountryName",
                schema: "dbo",
                table: "CityLocations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocaleCode",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnCities",
                schema: "dbo",
                columns: table => new
                {
                    ContinentName = table.Column<string>(maxLength: 64, nullable: false),
                    CountryName = table.Column<string>(maxLength: 128, nullable: false),
                    CityName = table.Column<string>(maxLength: 128, nullable: false),
                    Subdivision1Name = table.Column<string>(maxLength: 128, nullable: true),
                    Subdivision2Name = table.Column<string>(maxLength: 128, nullable: true),
                    LocaleCodeId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnCities", x => new { x.ContinentName, x.CountryName, x.CityName });
                    table.ForeignKey(
                        name: "FK_EnCities_LocaleCode_LocaleCodeId",
                        column: x => x.LocaleCodeId,
                        principalTable: "LocaleCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LocaleCode",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "en" },
                    { (short)2, "de" },
                    { (short)3, "fr" },
                    { (short)4, "es" },
                    { (short)5, "ru" },
                    { (short)6, "ja" },
                    { (short)7, "zh-CN" },
                    { (short)8, "pt-BR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_EnCityContinentName_EnCityCountryName_EnCityC~",
                schema: "dbo",
                table: "CityLocations",
                columns: new[] { "EnCityContinentName", "EnCityCountryName", "EnCityCityName" });

            migrationBuilder.CreateIndex(
                name: "IX_EnCities_LocaleCodeId",
                schema: "dbo",
                table: "EnCities",
                column: "LocaleCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityLocations_EnCities_EnCityContinentName_EnCityCountryNam~",
                schema: "dbo",
                table: "CityLocations",
                columns: new[] { "EnCityContinentName", "EnCityCountryName", "EnCityCityName" },
                principalSchema: "dbo",
                principalTable: "EnCities",
                principalColumns: new[] { "ContinentName", "CountryName", "CityName" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityLocations_EnCities_EnCityContinentName_EnCityCountryNam~",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropTable(
                name: "EnCities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LocaleCode");

            migrationBuilder.DropIndex(
                name: "IX_CityLocations_EnCityContinentName_EnCityCountryName_EnCityC~",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "EnCityCityName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "EnCityContinentName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.DropColumn(
                name: "EnCityCountryName",
                schema: "dbo",
                table: "CityLocations");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContinentName",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocaleCode",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subdivision1Name",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subdivision2Name",
                schema: "dbo",
                table: "CityLocations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
