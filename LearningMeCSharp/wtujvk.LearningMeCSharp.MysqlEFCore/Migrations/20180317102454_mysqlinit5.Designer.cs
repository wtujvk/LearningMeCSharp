﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;
using wtujvk.LearningMeCSharp.MysqlEFCore;

namespace wtujvk.LearningMeCSharp.MysqlEFCore.Migrations
{
    [DbContext(typeof(MySqldBContext))]
    [Migration("20180317102454_mysqlinit5")]
    partial class mysqlinit5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.Admin_Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsShow");

                    b.Property<bool>("Isexpand");

                    b.Property<DateTime>("OperTime");

                    b.Property<int>("OrderBy");

                    b.Property<int>("ParentID");

                    b.Property<string>("Remark")
                        .HasMaxLength(100);

                    b.Property<string>("SysAction")
                        .HasMaxLength(20);

                    b.Property<string>("SysArea")
                        .HasMaxLength(20);

                    b.Property<string>("SysController")
                        .HasMaxLength(20);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TreeId")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Admin_Menu");
                });

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.Admin_Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IsSys");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Remark")
                        .HasMaxLength(100);

                    b.Property<int>("RoleLevel");

                    b.Property<int>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("Admin_Role");
                });

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.Admin_RoleMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MenuId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("Admin_RoleMenu");
                });

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.Admin_UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Admin_UserRole");
                });

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.BsUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Password")
                        .HasMaxLength(64);

                    b.HasKey("ID");

                    b.ToTable("BsUser");
                });

            modelBuilder.Entity("wtujvk.LearningMeCSharp.Entities.YBackAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminRoleId");

                    b.Property<string>("BackPass")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("OperId");

                    b.Property<DateTime>("OperTime");

                    b.Property<int>("UserId");

                    b.Property<int>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("YBackAdmin");
                });
#pragma warning restore 612, 618
        }
    }
}
