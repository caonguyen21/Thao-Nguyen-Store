using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayDep.Models;
namespace WebBanGiayDep.Controllers
{
    public class HomeController : Controller
    {
        //Tao 1 doi tuong chua toan bo CSDL tu dbWeb ban giay
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        private List<SANPHAM> layGiayMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.ThoiGianBaoHanh).Take(count).ToList();
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
        /* public ActionResult ChiTietSanPham(int id)
         {
             var chitietsanpham = (from s in data.SANPHAMs
                                   where int.Parse(s.MaGiay) == id
                                   select (s.AnhBia, s.TenGiay, s.GiaBan, s.Size))
             return View(chitietsanpham);
         }*/
        [HttpGet]
        public ActionResult ThuongHieu()
        {
            var thuonghieu = (from s in data.SANPHAMs select (s.ThuongHieu.Distinct()));
            return PartialView(thuonghieu);
        }
    }
}