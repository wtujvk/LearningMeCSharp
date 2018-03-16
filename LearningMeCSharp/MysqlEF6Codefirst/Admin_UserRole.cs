namespace MysqlEF6Codefirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xyhisdemo.Admin_UserRole")]
    public partial class Admin_UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Role { get; set; }
    }
}
