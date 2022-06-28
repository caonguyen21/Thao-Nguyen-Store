using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayDep.Models;
using PagedList;
using System.IO;
using PagedList.Mvc;

namespace WebBanGiayDep.Controllers
{
    
    public class AdminController : Controller
    {
        dbShopGiayDataContext data = new dbShopGiayDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SanPham(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaGiay).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoiSanPham()
        {
            //lay ds tu table THUONGHIEU, sap xep theo Ten thuong hieu, chon lay gia tri MaThuongHieu, hien thi ten thuong hieu
            ViewBag.MaThuongHieu = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaThuongHieu", "TenThuongHieu");

            //lay ds tu table LoaiGiay, sap xep theo TenLoaiGiay, chon lay gia tri MaLoai, hien thi TenLoai
            ViewBag.MaLoai = new SelectList(data.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");

            //lay ds tu table NHACUNGCAP, sap xep theo TenNCC, chon lay gia tri MaNCC, hien thi TenNCC
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSanPham(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaThuongHieu = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaThuongHieu", "TenThuongHieu");
            ViewBag.MaLoai = new SelectList(data.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");

            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    //Kiem tra hinh anh co ton tai chua
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    sanpham.AnhBia = fileName;
                    //luu vao csdl
                    data.SANPHAMs.InsertOnSubmit(sanpham);
                    data.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }
        }

        //Hien Thi Chi Tiet San Pham
        public ActionResult ChiTietSanPham(int id)
        {
            // lay sp theo ma sp
            SANPHAM sanPham = data.SANPHAMs.SingleOrDefault(n => n.MaGiay == id);
            ViewBag.MaGiay = sanPham.MaGiay;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }

        [HttpGet]
        public ActionResult XoaSanPham(int id)
        {
            //lay san pham can xoa
            SANPHAM sanPham = data.SANPHAMs.SingleOrDefault(n => n.MaGiay == id);
            ViewBag.MaGiay = sanPham.MaGiay;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }
        [HttpPost,ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(int id)
        {
            SANPHAM sanPham = data.SANPHAMs.SingleOrDefault(n => n.MaGiay == id);
            ViewBag.MaGiay = sanPham.MaGiay;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(sanPham);
            data.SubmitChanges();
            return RedirectToAction("SanPham");
        }

        //Chinh sua San pham
        [HttpGet]
        public ActionResult SuaSanPham(int id)
        {
            SANPHAM sanPham = data.SANPHAMs.SingleOrDefault(n => n.MaGiay == id);
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //dua du lieu vao drop downlist TenTHuongHieu, TenLoai, TenNCC
            ViewBag.MaThuongHieu = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaThuongHieu", "TenThuongHieu");
            ViewBag.MaLoai = new SelectList(data.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            return View(sanPham);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham( int id, HttpPostedFileBase fileUpload)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.MaGiay == id);

            ViewBag.MaThuongHieu = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaThuongHieu", "TenThuongHieu");
            ViewBag.MaLoai = new SelectList(data.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    //Kiem tra hinh anh co ton tai chua
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    sp.AnhBia = fileName;
                    //luu vao csdl
                    UpdateModel(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("SanPham");
            }       
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                QUANLY ad = data.QUANLies.SingleOrDefault(n => n.TaiKhoanQL == tendn && n.MatKhau == matkhau);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        //y kien khach hang
        public ActionResult ykienkhachhang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(data.YKIENKHACHHANGs.ToList().OrderBy(n => n.MAYKIEN).ToPagedList(pageNumber, pageSize));
        }
        //Hien thi y kien
        [HttpGet]
        public ActionResult Xoaykienkhachhang(int id)
        {
            YKIENKHACHHANG ykienkhachhang = data.YKIENKHACHHANGs.SingleOrDefault(n => n.MAYKIEN == id);
            ViewBag.MAYKIEN = ykienkhachhang.MAYKIEN;
            if (ykienkhachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ykienkhachhang);
        }
        [HttpPost, ActionName("Xoaykienkhachhang")]
        public ActionResult Xacnhanxoa(int id)
        {
            //Lay ra y kien can xoa
            YKIENKHACHHANG ykienkhachhang = data.YKIENKHACHHANGs.SingleOrDefault(n => n.MAYKIEN == id);
            ViewBag.MAYKIEN = ykienkhachhang.MAYKIEN;
            if (ykienkhachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.YKIENKHACHHANGs.DeleteOnSubmit(ykienkhachhang);
            data.SubmitChanges();
            return RedirectToAction("Ykienkhachhang");
        }
    }
}