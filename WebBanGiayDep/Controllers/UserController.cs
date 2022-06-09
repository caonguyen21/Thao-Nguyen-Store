using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayDep.Models;

namespace WebBanGiayDep.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhaunhaplai = collection["MatkhauNhapLai"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var dienthoai = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            string matkhau_mahoa;
         

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ Tên khách hàng không được để trống";
            }else if(String.IsNullOrEmpty(tendn)){
                ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống!";
            }
            else if(!matkhaunhaplai.Equals(matkhau)){               
                ViewData["Loi4"] = "Mật khẩu nhập lại không trùng khớp!";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi6"] = "Địa chỉ không được để trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Số điện thoại không được để trống";
            }
            else if(String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi8"] = "Vui lòng nhập ngày sinh!!!";
            }
            else
            {
                matkhau_mahoa = Md5.MaHoaMD5(matkhau);
                kh.HoTen = hoten;
                kh.TaiKhoanKH = tendn;
                kh.MatKhau = matkhau_mahoa;
                kh.EmailKH = email;
                kh.DiaChiKH = diachi;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.DienThoaiKH = dienthoai;
                kh.TrangThai = true;

                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap","User");
            }

            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhau_mahoa = Md5.MaHoaMD5(matkhau);
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            }else if (String.IsNullOrEmpty(matkhau))
                    {
                        ViewData["Loi2"] = "Mật khẩu không được để trống";
                   }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoanKH == tendn && n.MatKhau == matkhau_mahoa);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}