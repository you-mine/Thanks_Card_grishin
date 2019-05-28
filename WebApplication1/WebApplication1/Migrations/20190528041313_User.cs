using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApplication1.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 4, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CD = table.Column<string>(maxLength: 6, nullable: true),
                    DepartmentName = table.Column<string>(maxLength: 20, nullable: true),
                    SectionName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 6, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CD = table.Column<string>(maxLength: 6, nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    Password = table.Column<string>(maxLength: 20, nullable: true),
                    UserName = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Hurigana = table.Column<string>(maxLength: 40, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThanksCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FromId = table.Column<int>(nullable: true),
                    ToId = table.Column<int>(nullable: true),
                    PostDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 20, nullable: true),
                    Body = table.Column<string>(maxLength: 100, nullable: true),
                    ThanksCount = table.Column<int>(nullable: false),
                    IsReaded = table.Column<bool>(nullable: false),
                    IsReadedAdmin = table.Column<bool>(nullable: false),
                    IsRepresentative = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanksCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanksCards_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThanksCards_Users_ToId",
                        column: x => x.ToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThanksCards_FromId",
                table: "ThanksCards",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanksCards_ToId",
                table: "ThanksCards",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThanksCards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
