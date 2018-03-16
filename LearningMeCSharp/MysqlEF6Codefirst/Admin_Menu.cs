namespace MysqlEF6Codefirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xyhisdemo.Admin_Menu")]
    public partial class Admin_Menu
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TreeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        public bool Isexpand { get; set; }

        [Required]
        [StringLength(200)]
        public string Url { get; set; }

        public int ParentID { get; set; }

        public int OrderBy { get; set; }

        public DateTime OperTime { get; set; }

        [StringLength(1073741823)]
        public string kqwUrl { get; set; }
    }
}
