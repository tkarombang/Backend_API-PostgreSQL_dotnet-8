using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class ProjectDeveloper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_developers_developers_DeveloperId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropForeignKey(
                name: "FK_project_developers_project_ProjectId",
                schema: "public",
                table: "project_developers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_developers",
                schema: "public",
                table: "project_developers");

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

            migrationBuilder.RenameTable(
                name: "project_developers",
                schema: "public",
                newName: "project_developer",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "Developer_id",
                schema: "public",
                table: "project_developer",
                newName: "developer_id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "public",
                table: "project_developer",
                newName: "project_id");

            migrationBuilder.AlterColumn<int>(
                name: "developer_id",
                schema: "public",
                table: "project_developer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_developer",
                schema: "public",
                table: "project_developer",
                columns: new[] { "project_id", "developer_id" });

            migrationBuilder.CreateIndex(
                name: "IX_project_developer_developer_id",
                schema: "public",
                table: "project_developer",
                column: "developer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_developer_developers_developer_id",
                schema: "public",
                table: "project_developer",
                column: "developer_id",
                principalSchema: "public",
                principalTable: "developers",
                principalColumn: "developer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_developer_project_project_id",
                schema: "public",
                table: "project_developer",
                column: "project_id",
                principalSchema: "public",
                principalTable: "project",
                principalColumn: "project_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_developer_developers_developer_id",
                schema: "public",
                table: "project_developer");

            migrationBuilder.DropForeignKey(
                name: "FK_project_developer_project_project_id",
                schema: "public",
                table: "project_developer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_developer",
                schema: "public",
                table: "project_developer");

            migrationBuilder.DropIndex(
                name: "IX_project_developer_developer_id",
                schema: "public",
                table: "project_developer");

            migrationBuilder.RenameTable(
                name: "project_developer",
                schema: "public",
                newName: "project_developers",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "developer_id",
                schema: "public",
                table: "project_developers",
                newName: "Developer_id");

            migrationBuilder.RenameColumn(
                name: "project_id",
                schema: "public",
                table: "project_developers",
                newName: "ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "Developer_id",
                schema: "public",
                table: "project_developers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                schema: "public",
                table: "project_developers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_developers",
                schema: "public",
                table: "project_developers",
                column: "Developer_id");

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
    }
}
