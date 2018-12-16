using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class NhanVienController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/NhanVien
        public ActionResult Index(int? page)
        {
            var kt = (NhanVien)Session["TaiKhoan"];
            if (kt.MaQuyen.Contains("Q001") || kt.MaQuyen.Contains("Q002"))
            {
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                return View(db.NhanViens.ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Index", "QuanLy");
        }
        //Thêm mới

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens.ToList(), "MaQuyen", "TenQuyen");
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(NhanVien nhanvien, HttpPostedFileBase anhDaidien)
        {
            if (anhDaidien == null)
            {
                ViewBag.Thongbao = "Chưa chọn hình ảnh.";
                return View();
            }
            //Lay file name
            var imagefileName = Path.GetFileName(anhDaidien.FileName);
            //Lưu đường dẫn ảnh
            var path = Path.Combine(Server.MapPath("~/Content/Images/NhanVien"), imagefileName);
            if (ModelState.IsValid)
            {
                NhanVien countNV = db.NhanViens.SingleOrDefault(n => n.MaNV == nhanvien.MaNV);
                if (countNV != null)
                {
                    ViewBag.KiemtraMa = "Mã nhân viên đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        anhDaidien.SaveAs(path);
                        nhanvien.Image = imagefileName;
                        db.NhanViens.Add(nhanvien);
                        db.SaveChanges();
                        ViewBag.Thanhcong = "Đã thêm thành công nhân viên.";
                    }  
                }

            }
            ViewBag.MaQuyen = new SelectList(db.Quyens.ToList(), "MaQuyen", "TenQuyen");
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaNV)
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens.ToList(), "MaQuyen", "TenQuyen");
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV== MaNV);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(NhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens.ToList(), "MaQuyen", "TenQuyen");
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string MaNV)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == MaNV);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaNV)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == MaNV);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaNV)
        {
            NhanVien nhanvien = db.NhanViens.SingleOrDefault(n => n.MaNV == MaNV);
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var nameAnhdaidien = nhanvien.Image;
            var path = Path.Combine(Server.MapPath("~/Content/Images/NhanVien"), nameAnhdaidien);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            db.NhanViens.Remove(nhanvien);
            db.SaveChanges();

            ViewBag.Daxoa = "Bạn đã xóa một Nhân viên khỏi danh sách";
            return RedirectToAction("Index");
        }
    }
}