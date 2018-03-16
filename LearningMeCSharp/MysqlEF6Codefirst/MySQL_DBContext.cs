namespace MysqlEF6Codefirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MySQL_DBContext : DbContext
    {
        public MySQL_DBContext()
            : base("name=MySQL_DBContext")
        {
        }

        public virtual DbSet<Admin_Menu> Admin_Menu { get; set; }
        public virtual DbSet<Admin_Role> Admin_Role { get; set; }
        public virtual DbSet<Admin_RoleMenu> Admin_RoleMenu { get; set; }
        public virtual DbSet<Admin_UserRole> Admin_UserRole { get; set; }
        public virtual DbSet<BsUser> BsUser { get; set; }
        public virtual DbSet<YBackAdmin> YBackAdmin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin_Menu>()
                .Property(e => e.TreeId)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_Menu>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_Menu>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_Menu>()
                .Property(e => e.kqwUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin_Role>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.F1)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.F2)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.F3)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.F4)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Introduce)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.PicturePath)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<BsUser>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<YBackAdmin>()
                .Property(e => e.BackPass)
                .IsUnicode(false);
        }
    }
}
