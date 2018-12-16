using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/KhacHang
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                KhachHang countKH = db.KhachHangs.SingleOrDefault(n => n.MaKH == kh.MaKH);
                if (countKH != null)
                {
                    ViewBag.KiemtraMa = "Mã Khách hàng đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    ViewBag.Thanhcong = "Đã thêm thành công Khách hàng.";

                }

            }
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}