namespace WebBanGiayDep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QUANLY")]
    public partial class QUANLY
    {
        [Key]
        [StringLength(50)]
        public string TaiKhoanQL { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string EmailQL { get; set; }

        [StringLength(10)]
        public string DienThoaiQL { get; set; }

        public bool? TrangThai { get; set; }
    }
}
