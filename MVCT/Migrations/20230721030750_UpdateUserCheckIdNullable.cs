using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCT.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCheckIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserCheckId",
                table: "Timesheets",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Timesheets",
                keyColumn: "UserCheckId",
                keyValue: null,
                column: "UserCheckId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserCheckId",
                table: "Timesheets",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
