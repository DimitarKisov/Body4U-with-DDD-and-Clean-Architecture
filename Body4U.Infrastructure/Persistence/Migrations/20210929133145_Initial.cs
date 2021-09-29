using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Body4U.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(maxLength: 500, nullable: false),
                    ShortBio = table.Column<string>(maxLength: 200, nullable: false),
                    FacebookUrl = table.Column<string>(nullable: false),
                    InstagramUrl = table.Column<string>(nullable: false),
                    YoutubeChannelUrl = table.Column<string>(nullable: false),
                    IsReadyToVisualize = table.Column<bool>(nullable: false, defaultValue: false),
                    IsReadyToWrite = table.Column<bool>(nullable: false, defaultValue: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Image = table.Column<byte[]>(nullable: false),
                    Content = table.Column<string>(maxLength: 25000, nullable: false),
                    Sources = table.Column<string>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: false),
                    ArticleType_Value = table.Column<int>(nullable: true),
                    TrainerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: false),
                    TrainerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerImage_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerVideo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoUrl = table.Column<string>(nullable: false),
                    TrainerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerVideo_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TrainerId",
                table: "Articles",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerImage_TrainerId",
                table: "TrainerImage",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerVideo_TrainerId",
                table: "TrainerVideo",
                column: "TrainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "TrainerImage");

            migrationBuilder.DropTable(
                name: "TrainerVideo");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
