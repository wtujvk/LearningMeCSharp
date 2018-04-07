namespace wtujvk.LearningMeCSharp.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

    [Table("BsUser")]
    public partial class BsUser:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 第三方code,手机号
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LoginName { get; set; }
        /// <summary>
        ///密码
        /// </summary>
        [StringLength(64)]
        public string Password { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
