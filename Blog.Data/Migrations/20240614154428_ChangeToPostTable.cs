using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Posts");

            migrationBuilder.AddColumn<List<string>>(
                name: "Contents",
                table: "Posts",
                type: "text[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostPhotoOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPhotoOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPhotoOption_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostPhotoOption_PostId",
                table: "PostPhotoOption",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostPhotoOption");

            migrationBuilder.DropColumn(
                name: "Contents",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
