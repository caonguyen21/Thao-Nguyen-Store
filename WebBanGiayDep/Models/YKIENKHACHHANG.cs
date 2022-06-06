namespace WebBanGiayDep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YKIENKHACHHANG")]
    public partial class YKIENKHACHHANG
    {
        [Key]
        public int MAYKIEN { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayGui { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public bool? TrangThai { get; set; }
    }
}
