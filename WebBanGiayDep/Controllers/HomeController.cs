using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayDep.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanGiayDep.Controllers
{
    public class HomeController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu dbWeb ban giay
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        public ActionResult Index()
        {
            return View();
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
        private List<SANPHAM> SoLuongTonGiay (int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.SoLuongTon).Take(count).ToList();
        }
        public ActionResult GiayTon()
        {
            var giayton = SoLuongTonGiay(9);
            return PartialView(giayton);
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
        [HttpGet]
        public ActionResult ThuongHieu()
        {
            var thuonghieu = (from s in data.THUONGHIEUs select s);
            return PartialView(thuonghieu);
        }

        public ActionResult SPTheoThuongHieu(int id)
        {
            var sanpham = from s in data.SANPHAMs where s.MaThuongHieu == id select s;
            return View(sanpham);
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

        public ActionResult SPTheoGioiTinh(int id)
        {
            var sanpham = from s in data.SANPHAMs where s.MaLoai == id select s;
            return View(sanpham);
        }
        public ActionResult TinTuc()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}