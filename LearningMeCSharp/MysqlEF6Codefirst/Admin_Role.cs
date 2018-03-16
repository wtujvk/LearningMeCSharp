namespace MysqlEF6Codefirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xyhisdemo.Admin_Role")]
    public partial class Admin_Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int WebSiteId { get; set; }

        public int IsSys { get; set; }

        public int RoleLevel { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }
    }
}
