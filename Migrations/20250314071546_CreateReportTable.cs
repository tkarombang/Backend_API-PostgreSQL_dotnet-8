using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "report",
                schema: "public",
                columns: table => new
                {
                    report_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    developer_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<int>(type: "integer", nullable: true),
                    task_id = table.Column<int>(type: "integer", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hours_spent = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.report_id);
                    table.ForeignKey(
                        name: "FK_report_developers_developer_id",
                        column: x => x.developer_id,
                        principalSchema: "public",
                        principalTable: "developers",
                        principalColumn: "developer_id");
                    table.ForeignKey(
                        name: "FK_report_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "public",
                        principalTable: "project",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_report_task_task_id",
                        column: x => x.task_id,
                        principalSchema: "public",
                        principalTable: "task",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_report_developer_id",
                schema: "public",
                table: "report",
                column: "developer_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_project_id",
                schema: "public",
                table: "report",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_task_id",
                schema: "public",
                table: "report",
                column: "task_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report",
                schema: "public");
        }
    }
}
