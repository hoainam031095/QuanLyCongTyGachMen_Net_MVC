using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class HoaDonXuatController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/HoaDonXuat
        public ActionResult Index(string MaKho)
        {
            var hd = from a in db.HoaDonXuats
                     join b in db.ChiTietHoaDonXuats
                     on a.MaHD equals b.MaHD
                     join c in db.SanPhams
                     on b.MaSP equals c.MaSp
                     where a.MaKho == MaKho
                     select new HoaDonXuatViewModels()
                     {
                         MaHD = a.MaHD,
                         MaSP = b.MaSP,
                         LyDo = a.LyDo,
                         TenNV = a.NhanVien.TenNV,
                         TenKho = a.Kho.TenKho,
                         TenKH = a.KhachHang.TenKH,
                         TenSP = b.SanPham.TenSp,
                         NguoiCho = a.NguoiCho,
                         BienXe = a.BienXe,
                         SoLuong = (int)(b.SoLuong),
                         DonGia = (int)(b.DonGia),
                         NgayXuat = a.NgayXuat,
                         ThanhTien = (int)(b.SoLuong) * (int)(b.DonGia)
                     };
            if (hd == null)
            {
                Response.StatusCode = 404;
            }
            var kt = (NhanVien)Session["TaiKhoan"];
            var nv = from a in db.NhanViens
                     join b in db.Khoes
                     on a.MaNV equals b.MaNV
                     where a.MaNV == kt.MaNV && b.MaKho == MaKho
                     select a;

            if (nv != null || kt.MaQuyen.Contains("Q001") || kt.MaQuyen.Contains("Q002"))
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
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList(), "MaKH", "TenKH");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(HoaDonXuat hd)
        {
            if (ModelState.IsValid)
            {
                HoaDonXuat countHD = db.HoaDonXuats.SingleOrDefault(n => n.MaHD == hd.MaHD);
                if (countHD != null)
                {
                    ViewBag.KiemtraMa = "Mã Hóa Đơn đã có hãy nhập lại hoặc đi đến chỉnh sửa";
                }
                else
                {
                    db.HoaDonXuats.Add(hd);
                    db.SaveChanges();
                    return RedirectToAction("ThemChiTietHD", "HoaDonXuat");
                }
            }
            ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
            ViewBag.MaNV = new SelectList(db.NhanViens.ToList(), "MaNV", "TenNV");
            ViewBag.MaKH = new SelectList(db.NhaCungCaps.ToList(), "MaKH", "TenKH");
            return View();
        }
        //Thêm chi tiết hóa đơn
        [HttpGet]
        public ActionResult ThemChiTietHD()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList(), "MaSP", "TenSP");
            return View();
        }
        [HttpPost]
        public ActionResult ThemChiTietHD(ChiTietHoaDonXuat cthd)
        {
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList(), "MaSP", "TenSP");
            var x = db.HoaDonXuats.ToList();
            cthd.MaHD = x.Last().MaHD;
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == cthd.MaSP);
            var y = sp.SoLuongTon - cthd.SoLuong;
            if (y < 0)
            {
                ViewBag.Hethang = "So luong Sp không đủ";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDonXuats.Add(cthd);
                db.SaveChanges();
                //SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == cthd.MaSP);
                sp.SoLuongTon = sp.SoLuongTon - cthd.SoLuong;
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Thanhcong = "Thêm thành công hóa đơn";
            }
            return View();
        }
        //Chỉnh sửa chi tiết hóa đơn
        [HttpGet]
        public ActionResult ChinhsuaCTHD(string MaHD)
        {
            ChiTietHoaDonXuat cthd = db.ChiTietHoaDonXuats.SingleOrDefault(n => n.MaHD == MaHD);
            if (cthd == null)
            {
                Response.StatusCode = 404;
            }
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList(), "MaSP", "TenSP");
            return View(cthd);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ChinhsuaCTHD(ChiTietHoaDonXuat cthd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cthd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.Dachinhsua = "Chỉnh sửa thành công chi tiết hóa đơn";
            }
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList(), "MaSP", "TenSP");
            return View();
        }

        //Xóa
        [HttpGet]
        public ActionResult Xoa(string MaHD, string MaSP)
        {
            var cthd = db.ChiTietHoaDonXuats.SingleOrDefault(n => n.MaHD == MaHD && n.MaSP == MaSP);
            if (cthd == null)
            {
                Response.StatusCode = 404;
            }
            return View(cthd);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacnhanXoa(string MaHD, string MaSP)
        {

            ChiTietHoaDonXuat cthdx = db.ChiTietHoaDonXuats.SingleOrDefault(n => n.MaHD == MaHD && n.MaSP == MaSP);
            HoaDonXuat hd = db.HoaDonXuats.SingleOrDefault(n => n.MaHD == MaHD);
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSp == cthdx.MaSP);
            if (cthdx != null)
            {
                db.ChiTietHoaDonXuats.Remove(cthdx);
                //db.SaveChanges();
                sanpham.SoLuongTon = sanpham.SoLuongTon + cthdx.SoLuong;
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                // db.SaveChanges();
                db.HoaDonXuats.Remove(hd);
                db.SaveChanges();
                ViewBag.Thanhcong = "Xóa Thành Công Hóa đơn";

            }
            return View();
        }
    }
}