using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class KhoController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/Kho
        public ActionResult Index()
        {
            //var kt = (NhanVien)Session["TaiKhoan"];
            //var
            //var kho = 
            return View(db.Khoes.ToList());
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(Kho kho)
        {
            if (ModelState.IsValid)
            {
                Kho countKho = db.Khoes.SingleOrDefault(n => n.MaKho == kho.MaKho);
                if (countKho != null)
                {
                    ViewBag.KiemtraMa = "Mã kho đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                        db.Khoes.Add(kho);
                        db.SaveChanges();
                        ViewBag.Thanhcong = "Đã thêm thành công kho.";
                        
                }

            }
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaKho)
        {
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == MaKho);
            if (kho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            return View(kho);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(Kho kho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kho).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string MaKho)
        {
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == MaKho);
            if (kho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kho);
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaKho)
        {
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == MaKho);
            if (kho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kho);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaKho)
        {
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == MaKho);
            if (kho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Khoes.Remove(kho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}