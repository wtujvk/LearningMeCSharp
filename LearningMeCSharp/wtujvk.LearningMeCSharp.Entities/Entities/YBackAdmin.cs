namespace wtujvk.LearningMeCSharp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

    [Table("YBackAdmin")]
    public partial class YBackAdmin:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(32)]
        public string BackPass { get; set; }

        public int OperId { get; set; }

        public DateTime OperTime { get; set; }

        public int WebSiteId { get; set; }

        public int? AdminRoleId { get; set; }
    }
}
