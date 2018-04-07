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
        /// �˵���
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TreeId { get; set; }
        /// <summary>
        ///��ʾ�ı�
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Text { get; set; }
        /// <summary>
        /// �Ƿ�չ��
        /// </summary>
        public bool Isexpand { get; set; }
        /// <summary>
        /// �˵�·��
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Url { get; set; }
        /// <summary>
        /// �ϼ��˵���0-���ϼ��˵�
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OperTime { get; set; }
        /// <summary>
        /// ��ע˵��
        /// </summary>
        [StringLength(100)]
        public string Remark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [StringLength(20)]
        public string SysArea { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        [StringLength(20)]
        public string SysController { get; set;}
        /// <summary>
        /// Action
        /// </summary>
        [StringLength(20)]
        public string SysAction { get; set; }
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public bool IsShow { get; set; }
        //[StringLength(250)]
        //public string kqwUrl { get; set; }
    }
}
