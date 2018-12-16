using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class VatLieuController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();

        // GET: Admin/VatLieu
        public ActionResult Index()
        {
            return View(db.VatLieux.ToList());
        }

        //Thêm mới

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(VatLieu vatlieu)
        {
            if (ModelState.IsValid)
            {
                VatLieu countvl = db.VatLieux.SingleOrDefault(n => n.MaVL == vatlieu.MaVL);
                if (countvl != null)
                {
                    ViewBag.KiemtraMa = "Mã Vật liệu đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    db.VatLieux.Add(vatlieu);
                    db.SaveChanges();
                    ViewBag.Thanhcong = "Đã thêm thành công vật liệu.";

                }

            }
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaVL)
        {
            VatLieu vatlieu = db.VatLieux.SingleOrDefault(n => n.MaVL == MaVL);
            if (vatlieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vatlieu);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(VatLieu vl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vl).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string MaVL)
        {
            VatLieu vatlieu = db.VatLieux.SingleOrDefault(n => n.MaVL == MaVL);
            if (vatlieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vatlieu);
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaVL)
        {
            VatLieu vatlieu = db.VatLieux.SingleOrDefault(n => n.MaVL == MaVL);
            if (vatlieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(vatlieu);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaVL)
        {
            VatLieu vatlieu = db.VatLieux.SingleOrDefault(n => n.MaVL == MaVL);
            if (vatlieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.VatLieux.Remove(vatlieu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}