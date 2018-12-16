using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
//using System.Text;
//using System.Security.Cryptography;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class QuanLyController : Controller
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/QuanLy
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection f)
        {
            string staikhoan = f["txttaikhoan"].ToString();
            string x = f["txtmatkhau"].ToString();
            NhanVien nv = db.NhanViens.SingleOrDefault(n => n.TaiKhoan == staikhoan && n.MatKhau == x);

            if (nv != null)
            {
                string dangnhap = nv.TenNV.ToString();
                string anhdaidien = nv.Image.ToString();
                Session["Ten"] = dangnhap;
                Session["Anh"] = anhdaidien;
                Session["TaiKhoan"] = nv;
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        public ActionResult DangXuat()
        {
            if (Session["TaiKhoan"] != null)
            {
                Session["TaiKhoan"] = null;
                Session["TenKhachHang"] = null;
            }
            return RedirectToAction("DangNhap", "QuanLy");
        }
    }
}