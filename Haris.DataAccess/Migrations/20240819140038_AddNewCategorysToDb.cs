using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Haris.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNewCategorysToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 1, "Action" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 2, "SciFi" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "History");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 2, "KTM 1190 Adventure S" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayOrder", "Name" },
                values: new object[] { 1, "BMW R 1200 GS Adventure" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Honda Africa Twin 1100");
        }
    }
}
