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
        /// ������code,�ֻ���
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Code { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// ��¼��
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LoginName { get; set; }
        /// <summary>
        ///����
        /// </summary>
        [StringLength(64)]
        public string Password { get; set; }
        /// <summary>
        /// �Ƿ񼤻�
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
