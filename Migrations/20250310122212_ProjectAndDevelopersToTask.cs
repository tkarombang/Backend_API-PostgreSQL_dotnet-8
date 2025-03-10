using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAndDevelopersToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project_id",
                schema: "public",
                table: "project_developers");

            migrationBuilder.RenameColumn(
                name: "developer_id",
                schema: "public",
                table: "project_developers",
                newName: "Developer_id");

            migrationBuilder.AlterColumn<string>(
                name: "Developer_id",
                schema: "public",
                table: "project_developers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                schema: "public",
                table: "project_developers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "public",
                table: "project_developers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                schema: "public",
                table: "project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                schema: "public",
                table: "project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "task",
                schema: "public",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    priority = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    developer_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_task_developers_developer_id",
                        column: x => x.developer_id,
                        principalSchema: "public",
                        principalTable: "developers",
                        principalColumn: "developer_id");
                    table.ForeignKey(
                        name: "FK_task_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "public",
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_developers_DeveloperId",
                schema: "public",
                table: "project_developers",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_project_developers_ProjectId",
                schema: "public",
                table: "project_developers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_task_developer_id",
                schema: "public",
                table: "task",
                column: "developer_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_project_id",
                schema: "public",
                table: "task",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_developers_developers_DeveloperId",
                schema: "public",
                table: "project_developers",
                column: "DeveloperId",
                principalSchema: "public",
                principalTable: "developers",
                principalColumn: "developer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_developers_project_ProjectId",
                schema: "public",
                table: "project_developers",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_developers_developers_DeveloperId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropForeignKey(
                name: "FK_project_developers_project_ProjectId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropTable(
                name: "task",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_project_developers_DeveloperId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropIndex(
                name: "IX_project_developers_ProjectId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.RenameColumn(
                name: "Developer_id",
                schema: "public",
                table: "project_developers",
                newName: "developer_id");

            migrationBuilder.AlterColumn<int>(
                name: "developer_id",
                schema: "public",
                table: "project_developers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "project_id",
                schema: "public",
                table: "project_developers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                schema: "public",
                table: "project",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                schema: "public",
                table: "project",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
