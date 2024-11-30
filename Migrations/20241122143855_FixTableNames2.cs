using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Developers",
                schema: "public",
                table: "Developers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectDevelopers",
                schema: "public",
                table: "ProjectDevelopers");

            migrationBuilder.RenameTable(
                name: "Developers",
                schema: "public",
                newName: "developers",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "ProjectDevelopers",
                schema: "public",
                newName: "project_developers",
                newSchema: "public");

            migrationBuilder.AlterColumn<int>(
                name: "tanggal_lahir",
                schema: "public",
                table: "developers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_developers",
                schema: "public",
                table: "developers",
                column: "developer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_developers",
                schema: "public",
                table: "project_developers",
                column: "developer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_developers",
                schema: "public",
                table: "developers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_developers",
                schema: "public",
                table: "project_developers");

            migrationBuilder.RenameTable(
                name: "developers",
                schema: "public",
                newName: "Developers",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "project_developers",
                schema: "public",
                newName: "ProjectDevelopers",
                newSchema: "public");

            migrationBuilder.AlterColumn<DateTime>(
                name: "tanggal_lahir",
                schema: "public",
                table: "Developers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Developers",
                schema: "public",
                table: "Developers",
                column: "developer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectDevelopers",
                schema: "public",
                table: "ProjectDevelopers",
                column: "developer_id");
        }
    }
}
