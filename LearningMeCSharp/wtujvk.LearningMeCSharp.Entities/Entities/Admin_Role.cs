namespace wtujvk.LearningMeCSharp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

    [Table("Admin_Role")]
    public partial class Admin_Role: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
