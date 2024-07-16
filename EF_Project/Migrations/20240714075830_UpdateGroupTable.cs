using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 11, 58, 29, 620, DateTimeKind.Local).AddTicks(201));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 1, 14, 11, 58, 29, 620, DateTimeKind.Local).AddTicks(844));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Groups");
        }
    }
}
