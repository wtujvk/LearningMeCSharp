using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using wtujvk.LearningMeCSharp.Entities;
using wtujvk.LearningMeCSharp.MysqlEFCore.ext;

namespace wtujvk.LearningMeCSharp.MysqlEFCore
{
    public partial class MySqldBContext:DbContext
    {
        public MySqldBContext()
        {
            DbInitializer.Initialize(this);
        }
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="conn"></param>
        public static void SetConn(string conn = null)
        {
            if (!string.IsNullOrWhiteSpace(conn))
            {
                MysqlConn = conn;
            }
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string MysqlConn = "server=192.168.1.90;user id=root;password=admin;persistsecurityinfo=True;database=xyhisdemo5;SslMode=None";
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Admin_Menu> Admin_Menu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Admin_Role> Admin_Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Admin_RoleMenu> Admin_RoleMenu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Admin_UserRole> Admin_UserRole { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<BsUser> BsUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<YBackAdmin> YBackAdmin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<BearerEntity> BearerEntity { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(MysqlConn))
            {
                optionsBuilder.UseMySQL(MysqlConn);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
