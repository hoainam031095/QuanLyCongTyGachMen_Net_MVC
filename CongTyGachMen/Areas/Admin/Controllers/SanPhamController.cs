using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;
using System.IO;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            return View(db.SanPhams.ToList());
        }

        //Thêm mới

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Chưa chọn hình ảnh.";
                return View();
            }
            //Lay file name
            var imagefileName = Path.GetFileName(fileUpload.FileName);
            //Lưu đường dẫn ảnh
            var path = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), imagefileName);
            if(ModelState.IsValid)
            {
                SanPham countSP = db.SanPhams.SingleOrDefault(n => n.MaSp == sanpham.MaSp);
                if(countSP != null)
                {
                    ViewBag.KiemtraMa = "Mã sản phẩm đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn  tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                        sanpham.Image = imagefileName;
                        db.SanPhams.Add(sanpham);
                        db.SaveChanges();
                        ViewBag.Thanhcong = "Đã thêm thành công sản phẩm.";
                    }  
                }
               
            }
            return View();
        }

        //Chỉnh sửa
        [HttpGet]
        public ActionResult Chinhsua(string MaSp)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSp == MaSp);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Chinhsua(SanPham sanpham)
        {
            if(ModelState.IsValid)
            {
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Đã chỉnh sửa thành công";
            }
            return View();
        }

        //Hien thị

        public ActionResult Hienthi(string maSp)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSp == maSp);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        //Xóa
        [HttpGet]
        public ActionResult Delete(string maSp)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSp == maSp);
            if(sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult XacnhanXoa(string maSp)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSp == maSp);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();

            ViewBag.Daxoa = "Bạn đã xóa một Sản Phẩm khỏi danh sách";
            return RedirectToAction("Index");
        }
    }
}