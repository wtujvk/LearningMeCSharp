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
        /// <summary>
        /// 菜单树
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TreeId { get; set; }
        /// <summary>
        ///显示文本
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Isexpand { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Url { get; set; }
        /// <summary>
        /// 上级菜单，0-无上级菜单
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime OperTime { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        [StringLength(100)]
        public string Remark { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [StringLength(20)]
        public string SysArea { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        [StringLength(20)]
        public string SysController { get; set;}
        /// <summary>
        /// Action
        /// </summary>
        [StringLength(20)]
        public string SysAction { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        //[StringLength(250)]
        //public string kqwUrl { get; set; }
    }
}
