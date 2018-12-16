using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class HoaDonNhapController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/HoaDonNhap
        public ActionResult Index(string MaKho)
        {
            var hd = from a in db.HoaDonNhaps
                     join b in db.ChiTietHoaDonNhaps
                     on a.MaHD equals b.MaHD
                     join c in db.VatLieux
                     on b.MaVL equals c.MaVL
                     where a.MaKho == MaKho
                     select new HoaDonViewModels()
                     {
                         MaHD = a.MaHD,
                         MaVL = b.MaVL,
                         LyDo = a.LyDo,
                         TenNV = a.NhanVien.TenNV,
                         TenKho = a.Kho.TenKho,
                         TenNCC = a.NhaCungCap.TenNCC,
                         TenVL = b.VatLieu.TenVL,
                         NguoiCho = a.NguoiCho,
                         BienXe = a.BienXe,
                         SoLuong = (int)(b.SoLuong),
                         DonGia = (int)(b.Dongia),
                         NgayNhap = a.NgayNhap,
                         ThanhTien = (int)(b.SoLuong) * (int)(b.Dongia)
                     };
            if (hd == null)
            {
                Response.StatusCode = 404;
            }
            var kt = (NhanVien)Session["TaiKhoan"];
            Kho kho = db.Khoes.SingleOrDefault(n => n.MaKho == MaKho);

            if (kt.MaNV.Contains(kho.MaNV)|| kt.MaQuyen.Contains("Q001") || kt.MaQuyen.Contains("Q002"))
            {
                return View(hd);
            }
            return RedirectToAction("Index", "QuanLy");
        }

        //Thêm hóa đơn nhập
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(HoaDonNhap hd)
        {
            if (ModelState.IsValid)
            {
                HoaDonNhap countHD = db.HoaDonNhaps.SingleOrDefault(n => n.MaHD == hd.MaHD);
                if (countHD != null)
                {
                    ViewBag.KiemtraMa = "Mã Hóa Đơn đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    db.HoaDonNhaps.Add(hd);
                    db.SaveChanges();
                    return RedirectToAction("ThemChiTietHD", "HoaDonNhap");
                }
            }
            ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
            return View();
        }
        //Thêm chi tiết hóa đơn
        [HttpGet]
        public ActionResult ThemChiTietHD()
        {
            ViewBag.MaVL = new SelectList(db.VatLieux.ToList(), "MaVL", "TenVL");
            return View();
        } 
        [HttpPost]
        public ActionResult ThemChiTietHD(ChiTietHoaDonNhap cthd)
        {
            ViewBag.MaVL = new SelectList(db.VatLieux.ToList(), "MaVL", "TenVL");
            var x = db.HoaDonNhaps.ToList();
            cthd.MaHD = x.Last().MaHD;
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDonNhaps.Add(cthd);
                db.SaveChanges();
                VatLieu vl = db.VatLieux.SingleOrDefault(n => n.MaVL == cthd.MaVL);
                vl.SoLuongTon = vl.SoLuongTon + cthd.SoLuong;
                db.Entry(vl).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Thanhcong = "Thêm thành công hóa đơn";
            }
            return View();
        }
        //Chỉnh sử chi tiết hóa đơn
        [HttpGet]
        public ActionResult ChinhsuaCTHD(string MaHD)
        {
            ChiTietHoaDonNhap cthd = db.ChiTietHoaDonNhaps.SingleOrDefault(n => n.MaHD == MaHD);
            if (cthd == null)
            {
                Response.StatusCode = 404;
            }
            ViewBag.MaVL = new SelectList(db.VatLieux.ToList(), "MaVL", "TenVL");
            return View(cthd);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ChinhsuaCTHD(ChiTietHoaDonNhap cthd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cthd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Thanhcong = "Chỉnh sửa thành công chi tiết hóa đơn";
            }
            ViewBag.MaVL = new SelectList(db.VatLieux.ToList(), "MaVL", "TenVL");
            return View();
        }

        //[HttpGet]
        //public ActionResult ChinhsuaHD(string MaHD)
        //{
        //    HoaDonNhap hd = db.HoaDonNhaps.SingleOrDefault(n => n.MaHD == MaHD);
        //    if (hd == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
        //    ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
        //    return View(hd);
        //}
        //chỉnh sửa HD
        //[HttpPost]
        //[ValidateInput(false)]

        //public ActionResult ChinhsuaHD(HoaDonNhap hd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("ChinhsuaCTHD", "HoaDonNhap");

        //    }
        //    ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
        //    ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
        //    return View();
        //}


        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaHD, string MaVL)
        {
            var cthd = db.ChiTietHoaDonNhaps.SingleOrDefault(n => n.MaHD == MaHD && n.MaVL == MaVL);
            if (cthd == null)
            {
                Response.StatusCode = 404;
            }
            return View(cthd);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaHD, string MaVL)
        {

            ChiTietHoaDonNhap ctpn = db.ChiTietHoaDonNhaps.SingleOrDefault(n => n.MaHD == MaHD && n.MaVL == MaVL);
            HoaDonNhap hd = db.HoaDonNhaps.SingleOrDefault(n => n.MaHD == MaHD);
            VatLieu vatlieu = db.VatLieux.SingleOrDefault(n => n.MaVL == ctpn.MaVL);
            if (ctpn != null)
            {
                db.ChiTietHoaDonNhaps.Remove(ctpn);
                //db.SaveChanges();
                vatlieu.SoLuongTon = vatlieu.SoLuongTon - ctpn.SoLuong;
                db.Entry(vatlieu).State = System.Data.Entity.EntityState.Modified;
                // db.SaveChanges();
                db.HoaDonNhaps.Remove(hd);
                db.SaveChanges();
                ViewBag.Thanhcong = "Xóa Thành Công Hóa đơn";
                
            }
            return View();
        }
    }

}
