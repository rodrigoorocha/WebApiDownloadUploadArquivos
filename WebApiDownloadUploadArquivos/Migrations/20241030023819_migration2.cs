using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDownloadUploadArquivos.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FilesModels",
                table: "FilesModels");

            migrationBuilder.RenameTable(
                name: "FilesModels",
                newName: "FileModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel");

            migrationBuilder.RenameTable(
                name: "FileModel",
                newName: "FilesModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilesModels",
                table: "FilesModels",
                column: "Id");
        }
    }
}
