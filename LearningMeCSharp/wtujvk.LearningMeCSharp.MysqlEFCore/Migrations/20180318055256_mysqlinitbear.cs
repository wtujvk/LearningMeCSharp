using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wtujvk.LearningMeCSharp.MysqlEFCore.Migrations
{
    public partial class mysqlinitbear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BearerEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Access_token = table.Column<string>(maxLength: 64, nullable: true),
                    Access_token_Expires = table.Column<DateTime>(nullable: false),
                    AppName = table.Column<string>(maxLength: 120, nullable: true),
                    AppSecret = table.Column<string>(maxLength: 250, nullable: true),
                    Refresh_Token_Expires = table.Column<DateTime>(nullable: false),
                    Refresh_token = table.Column<string>(maxLength: 64, nullable: true),
                    StateCode = table.Column<short>(nullable: false),
                    UserId = table.Column<string>(maxLength: 64, nullable: true),
                    UserName = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BearerEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BearerEntity");
        }
    }
}
