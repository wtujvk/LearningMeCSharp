namespace MysqlEF6Codefirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xyhisdemo.Admin_RoleMenu")]
    public partial class Admin_RoleMenu
    {
        public int Id { get; set; }

        public int Role { get; set; }

        public int Menu { get; set; }
    }
}
