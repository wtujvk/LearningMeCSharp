using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using wtujvk.LearningMeCSharp.Entities;

namespace wtujvk.LearningMeCSharp.MysqlEFCore
{
    public partial class MySqldBContext:DbContext
    {
        //public MySqldBContext()
        //{

        //}
        private static string MysqlConn = "server=192.168.1.90;user id=root;password=admin;persistsecurityinfo=True;database=xyhisdemo4;SslMode=None";
        public virtual DbSet<Admin_Menu> Admin_Menu { get; set; }
        public virtual DbSet<Admin_Role> Admin_Role { get; set; }
        public virtual DbSet<Admin_RoleMenu> Admin_RoleMenu { get; set; }
        public virtual DbSet<Admin_UserRole> Admin_UserRole { get; set; }
        public virtual DbSet<BsUser> BsUser { get; set; }
        public virtual DbSet<YBackAdmin> YBackAdmin { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(MysqlConn);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
