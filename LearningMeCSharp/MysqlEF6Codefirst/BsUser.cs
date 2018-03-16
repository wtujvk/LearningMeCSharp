namespace MysqlEF6Codefirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xyhisdemo.BsUser")]
    public partial class BsUser
    {
        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string Code { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(64)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [StringLength(1073741823)]
        public string Reason { get; set; }

        public short LsInputWay { get; set; }

        [StringLength(100)]
        public string F1 { get; set; }

        [StringLength(100)]
        public string F2 { get; set; }

        [StringLength(100)]
        public string F3 { get; set; }

        [StringLength(100)]
        public string F4 { get; set; }

        public short IconIndex { get; set; }

        public bool? IsUserInputWB { get; set; }

        public bool? IsUserInputPY { get; set; }

        public bool? IsUserInputCode { get; set; }

        public bool? IsUserInputName { get; set; }

        public bool? IsUserInputStrokeCode { get; set; }

        public bool? IsUserInputEngDesc { get; set; }

        [StringLength(4000)]
        public string Introduce { get; set; }

        [StringLength(50)]
        public string PicturePath { get; set; }

        [StringLength(40)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        public int? LevelId { get; set; }

        public int DocLevId { get; set; }

        public int? HospitalId { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }
    }
}
