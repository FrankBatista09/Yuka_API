using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YukaDAL.Migrations
{
    /// <inheritdoc />
    public partial class SizeCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SizeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SizeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "SizeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "SizeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SizeCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "SizeCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SizeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SizeCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SizeCategories");
        }
    }
}
