namespace wtujvk.LearningMeCSharp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("Admin_Menu")]
    public partial class Admin_Menu: IEntity
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity)]
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

        [StringLength(250)]
        public string kqwUrl { get; set; }
    }
}
