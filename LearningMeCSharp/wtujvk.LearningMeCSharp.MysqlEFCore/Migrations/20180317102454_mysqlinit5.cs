using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace wtujvk.LearningMeCSharp.MysqlEFCore.Migrations
{
    public partial class mysqlinit5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kqwUrl",
                table: "Admin_Menu");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Admin_UserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Admin_RoleMenu",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Menu",
                table: "Admin_RoleMenu",
                newName: "MenuId");

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Admin_Menu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Admin_Menu",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SysAction",
                table: "Admin_Menu",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SysArea",
                table: "Admin_Menu",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SysController",
                table: "Admin_Menu",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Admin_Menu");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Admin_Menu");

            migrationBuilder.DropColumn(
                name: "SysAction",
                table: "Admin_Menu");

            migrationBuilder.DropColumn(
                name: "SysArea",
                table: "Admin_Menu");

            migrationBuilder.DropColumn(
                name: "SysController",
                table: "Admin_Menu");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Admin_UserRole",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Admin_RoleMenu",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Admin_RoleMenu",
                newName: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "kqwUrl",
                table: "Admin_Menu",
                maxLength: 250,
                nullable: true);
        }
    }
}
