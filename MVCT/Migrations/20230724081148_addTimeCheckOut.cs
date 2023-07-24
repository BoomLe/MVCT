using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCT.Migrations
{
    /// <inheritdoc />
    public partial class addTimeCheckOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCheckout",
                table: "Timesheets",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeCheckout",
                table: "Timesheets");
        }
    }
}
