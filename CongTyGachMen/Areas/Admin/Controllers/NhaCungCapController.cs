using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class NhaCungCapController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/NhaCungCap
        public ActionResult Index()
        {
            return View(db.NhaCungCaps.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                NhaCungCap countNcc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == ncc.MaNCC);
                if (countNcc != null)
                {
                    ViewBag.KiemtraMa = "Mã Nhà cung cấp đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    db.NhaCungCaps.Add(ncc);
                    db.SaveChanges();
                    ViewBag.Thanhcong = "Đã thêm thành công Nhà cung cấp.";

                }

            }
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaNCC)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string MaNCC)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaNCC)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaNCC)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
