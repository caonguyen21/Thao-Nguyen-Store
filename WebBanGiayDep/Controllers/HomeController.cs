using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanGiayDep.Models;

namespace WebBanGiayDep.Controllers
{
    public class HomeController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu dbWeb ban giay
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var giayton = SoLuongTonGiay(50);
            return PartialView(giayton.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ListSanPham(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var giayton = SoLuongTonGiay(50);
            return PartialView(giayton.ToPagedList(pageNum, pageSize));
        }
        private List<SANPHAM> layGiayMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult GiayMoi()
        {
            var giaymoi = layGiayMoi(5);
            return PartialView(giaymoi);
        }
        private List<SANPHAM> SoLuongTonGiay(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.SoLuongTon).Take(count).ToList();
        }
        public ActionResult ChiTietSanPham(int id)
        {
            var chitietsanpham = (from s in data.SANPHAMs
                                  where s.MaGiay == id
                                  join lg in data.LOAIGIAYs
                                  on s.MaLoai equals lg.MaLoai
                                  join th in data.THUONGHIEUs
                                  on s.MaThuongHieu equals th.MaThuongHieu
                                  select new CTSP
                                  {
                                      MaGiay = s.MaGiay,
                                      TenGiay = s.TenGiay,
                                      Size = s.Size,
                                      AnhBia = s.AnhBia,
                                      GiaBan = s.GiaBan,
                                      MaThuongHieu = th.MaThuongHieu,
                                      TenThuongHieu = th.TenThuongHieu,
                                      MaLoai = lg.MaLoai,
                                      TenLoai = lg.TenLoai,
                                      ThoiGianBaoHanh = (int)s.ThoiGianBaoHanh,
                                  });
            return View(chitietsanpham.Single());
        }
        public ActionResult ThuongHieu()
        {
            var thuonghieu = (from s in data.THUONGHIEUs select s);
            return PartialView(thuonghieu);
        }

        public ActionResult SPTheoThuongHieu(int id, int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanpham = from s in data.SANPHAMs where (s.MaThuongHieu == id && s.TrangThai == true) select s;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }
        public ActionResult GiayNam()
        {
            var GiayNam = from s in data.LOAIGIAYs
                          where s.GioiTinh == true
                          select s;
            return PartialView(GiayNam);
        }
        public ActionResult GiayNu()
        {
            var GiayNu = from s in data.LOAIGIAYs
                         where s.GioiTinh == false
                         select s;
            return PartialView(GiayNu);
        }

        public ActionResult SPTheoGioiTinh(int id, int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanpham = from s in data.SANPHAMs where (s.MaLoai == id && s.TrangThai == true) select s;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }
        public ActionResult TinTuc()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        #region Sản phẩm tìm kiếm (Search)
        public ActionResult Search(string id)
        {
            //Lấy ra danh sách sản phẩm từ chuỗi tìm kiếm truyền vào
            var search = (from sp in data.SANPHAMs
                          where (sp.TrangThai == true) && (sp.TenGiay.Contains(id)) && (sp.THUONGHIEU.TenThuongHieu.Contains(id))
                          orderby sp.GiaBan descending
                          select sp).ToList();

            //Lấy ra các sản phẩm khác (Không có sản phẩm của nhà sản xuất đang xem)
            ViewBag.SP_Khac = (from sp in data.SANPHAMs
                               where (sp.TrangThai == true) && (!sp.TenGiay.Contains(id)) && (sp.THUONGHIEU.TenThuongHieu.Contains(id))
                               orderby sp.GiaBan descending
                               select sp).ToList();

            //Tạo key để xuất thông báo tìm kiếm
            ViewBag.TuKhoa = id;
            return View(search);
        }
        #endregion
    }
}