namespace WebBanGiayDep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CT_DONHANG = new HashSet<CT_DONHANG>();
        }

        [Key]
        public int MaGiay { get; set; }

        [Required]
        [StringLength(50)]
        public string TenGiay { get; set; }

        public byte Size { get; set; }

        [StringLength(50)]
        public string AnhBia { get; set; }

        public decimal GiaBan { get; set; }

        public int? MaThuongHieu { get; set; }

        public bool? TrangThai { get; set; }

        public int? MaNCC { get; set; }

        public int? MaLoai { get; set; }

        public int? ThoiGianBaoHanh { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int SoLuongTon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DONHANG> CT_DONHANG { get; set; }

        public virtual LOAIGIAY LOAIGIAY { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }
}
