using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wtujvk.LearningMeCSharp.MysqlEFCore.Migrations
{
    public partial class mysqlinit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin_Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Isexpand = table.Column<bool>(nullable: false),
                    OperTime = table.Column<DateTime>(nullable: false),
                    OrderBy = table.Column<int>(nullable: false),
                    ParentID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 50, nullable: false),
                    TreeId = table.Column<string>(maxLength: 100, nullable: false),
                    Url = table.Column<string>(maxLength: 200, nullable: false),
                    kqwUrl = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    IsSys = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    RoleLevel = table.Column<int>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin_RoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Menu = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_RoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admin_UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Role = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BsUser",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Address = table.Column<string>(maxLength: 40, nullable: true),
                    Code = table.Column<string>(maxLength: 32, nullable: false),
                    DocLevId = table.Column<int>(nullable: false),
                    F1 = table.Column<string>(maxLength: 100, nullable: true),
                    F2 = table.Column<string>(maxLength: 100, nullable: true),
                    F3 = table.Column<string>(maxLength: 100, nullable: true),
                    F4 = table.Column<string>(maxLength: 100, nullable: true),
                    HospitalId = table.Column<int>(nullable: true),
                    IconIndex = table.Column<short>(nullable: false),
                    Introduce = table.Column<string>(maxLength: 4000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsUserInputCode = table.Column<bool>(nullable: true),
                    IsUserInputEngDesc = table.Column<bool>(nullable: true),
                    IsUserInputName = table.Column<bool>(nullable: true),
                    IsUserInputPY = table.Column<bool>(nullable: true),
                    IsUserInputStrokeCode = table.Column<bool>(nullable: true),
                    IsUserInputWB = table.Column<bool>(nullable: true),
                    LevelId = table.Column<int>(nullable: true),
                    LsInputWay = table.Column<short>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 15, nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: true),
                    PicturePath = table.Column<string>(maxLength: 50, nullable: true),
                    Reason = table.Column<string>(maxLength: 250, nullable: true),
                    X = table.Column<double>(nullable: true),
                    Y = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BsUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YBackAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AdminRoleId = table.Column<int>(nullable: true),
                    BackPass = table.Column<string>(maxLength: 32, nullable: false),
                    OperId = table.Column<int>(nullable: false),
                    OperTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YBackAdmin", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_Menu");

            migrationBuilder.DropTable(
                name: "Admin_Role");

            migrationBuilder.DropTable(
                name: "Admin_RoleMenu");

            migrationBuilder.DropTable(
                name: "Admin_UserRole");

            migrationBuilder.DropTable(
                name: "BsUser");

            migrationBuilder.DropTable(
                name: "YBackAdmin");
        }
    }
}
