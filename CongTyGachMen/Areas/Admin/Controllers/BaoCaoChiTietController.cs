using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongTyGachMen.Models;

namespace CongTyGachMen.Areas.Admin.Controllers
{
    public class BaoCaoChiTietController : BaseController
    {
        QuanLyCtyEntities14 db = new QuanLyCtyEntities14();
        // GET: Admin/BaoCaoChiTiet

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
            ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
            var kt = (NhanVien)Session["TaiKhoan"];
            if (kt.MaQuyen.Contains("Q002"))
            {
                return View();
            }
            return RedirectToAction("Index", "QuanLy");
        }

        [HttpPost]
        public ActionResult BaocaoNhap(FormCollection f)
        {
            String tungay = f["Tungay"].ToString();
            String denngay = f["Denngay"].ToString();

            int riengchothang = int.Parse(f["Riengchothang"].ToString());
            int riengchothangnam = int.Parse(f["Riengchothangnam"].ToString());

            int riengchoquy = int.Parse(f["Riengchoquy"].ToString());
            int riengchoquynam = int.Parse(f["Riengchoquynam"].ToString());

            int tronnam = int.Parse(f["Tronnam"].ToString());

            string MaNCC = f["MaNCC"].ToString();
            string MaKho = f["MaKho"].ToString();
            string Hinhthuc = f["Hoadon"].ToString();

            string x = f["radio"].ToString();
            if (x == "Theongay")
            {
                DateTime tn = Convert.ToDateTime(tungay);
                DateTime dn = Convert.ToDateTime(denngay);
                var hd = from a in db.HoaDonNhaps
                             join b in db.ChiTietHoaDonNhaps
                             on a.MaHD equals b.MaHD
                             join c in db.VatLieux
                             on b.MaVL equals c.MaVL
                             where a.NgayNhap > tn && a.NgayNhap < dn && a.MaKho == MaKho && a.MaNCC == MaNCC
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
                return View(hd);

            }
            else
            {
                if (x == "Theothang")
                {
                   
                        var hd = from a in db.HoaDonNhaps
                                 join b in db.ChiTietHoaDonNhaps
                                 on a.MaHD equals b.MaHD
                                 join c in db.VatLieux
                                 on b.MaVL equals c.MaVL
                                 where a.NgayNhap.Value.Month == riengchothang && a.NgayNhap.Value.Year == riengchothangnam && a.MaKho == MaKho && a.MaNCC == MaNCC
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
                    return View(hd);

                }
                else
                {
                    if (x == "Theoquy")
                    {
                        int y, z, t;

                        if (riengchoquy == 1)
                        {
                            y = 1; z = 2; t = 3;
                        }
                        else
                        {
                            if (riengchoquy == 2)
                            {
                                y = 4; z = 5; t = 6;
                            }
                            else
                            {
                                if (riengchoquy == 3)
                                {
                                    y = 7; z = 9; t = 9;
                                }
                                else
                                {
                                    y = 10; z = 11; t = 12;
                                }
                            }
                        }
                        
                            var hd = from a in db.HoaDonNhaps
                                     join b in db.ChiTietHoaDonNhaps
                                     on a.MaHD equals b.MaHD
                                     join c in db.VatLieux
                                     on b.MaVL equals c.MaVL
                                     where (a.NgayNhap.Value.Month == y || a.NgayNhap.Value.Month == z || a.NgayNhap.Value.Month == t) && a.NgayNhap.Value.Year == riengchoquynam && a.MaKho == MaKho && a.MaNCC == MaNCC
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
                        return View(hd);

                    }
                    else
                    {
                        var hd = from a in db.HoaDonNhaps
                                     join b in db.ChiTietHoaDonNhaps
                                     on a.MaHD equals b.MaHD
                                     join c in db.VatLieux
                                     on b.MaVL equals c.MaVL
                                     where a.NgayNhap.Value.Year == tronnam && a.MaKho == MaKho && a.MaNCC == MaNCC
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
                        return View(hd);
                        
                    }
                }
            }
        }
        [HttpGet]
        public ActionResult IndexHDXuat()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList(), "MaKH", "TenKH");
            ViewBag.MaKho = new SelectList(db.Khoes.ToList(), "MaKho", "TenKho");
            var kt = (NhanVien)Session["TaiKhoan"];
            if (kt.MaQuyen.Contains("Q002"))
            {
                return View();
            }
            return RedirectToAction("Index", "QuanLy");
        }

        [HttpPost]
        public ActionResult BaocaoXuat(FormCollection f)
        {
            String tungay = f["Tungay"].ToString();
            String denngay = f["Denngay"].ToString();

            int riengchothang = int.Parse(f["Riengchothang"].ToString());
            int riengchothangnam = int.Parse(f["Riengchothangnam"].ToString());

            int riengchoquy = int.Parse(f["Riengchoquy"].ToString());
            int riengchoquynam = int.Parse(f["Riengchoquynam"].ToString());

            int tronnam = int.Parse(f["Tronnam"].ToString());

            string MaKH = f["MaKH"].ToString();
            string MaKho = f["MaKho"].ToString();
            string Hinhthuc = f["Hoadon"].ToString();

            string x = f["radio"].ToString();
            if (x == "Theongay")
            {
                DateTime tn = Convert.ToDateTime(tungay);
                DateTime dn = Convert.ToDateTime(denngay);
                var hd = from a in db.HoaDonXuats
                         join b in db.ChiTietHoaDonXuats
                         on a.MaHD equals b.MaHD
                         join c in db.SanPhams
                         on b.MaSP equals c.MaSp
                         where a.NgayXuat > tn && a.NgayXuat < dn && a.MaKho == MaKho && a.MaKH == MaKH
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
                return View(hd);

            }
            else
            {
                if (x == "Theothang")
                {
                    var hd = from a in db.HoaDonXuats
                             join b in db.ChiTietHoaDonXuats
                             on a.MaHD equals b.MaHD
                             join c in db.SanPhams
                             on b.MaSP equals c.MaSp
                             where a.NgayXuat.Value.Month == riengchothang && a.NgayXuat.Value.Year == riengchothangnam && a.MaKho == MaKho && a.MaKH == MaKH
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
                    return View(hd);

                }
                else
                {
                    if (x == "Theoquy")
                    {
                        int y, z, t;

                        if (riengchoquy == 1)
                        {
                            y = 1; z = 2; t = 3;
                        }
                        else
                        {
                            if (riengchoquy == 2)
                            {
                                y = 4; z = 5; t = 6;
                            }
                            else
                            {
                                if (riengchoquy == 3)
                                {
                                    y = 7; z = 9; t = 9;
                                }
                                else
                                {
                                    y = 10; z = 11; t = 12;
                                }
                            }
                        }

                        var hd = from a in db.HoaDonXuats
                                 join b in db.ChiTietHoaDonXuats
                                 on a.MaHD equals b.MaHD
                                 join c in db.SanPhams
                                 on b.MaSP equals c.MaSp
                                 where (a.NgayXuat.Value.Month == y || a.NgayXuat.Value.Month == z || a.NgayXuat.Value.Month == t) && a.NgayXuat.Value.Year == riengchoquynam && a.MaKho == MaKho && a.MaKH == MaKH
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
                        return View(hd);

                    }
                    else
                    {
                        var hd = from a in db.HoaDonXuats
                                 join b in db.ChiTietHoaDonXuats
                                 on a.MaHD equals b.MaHD
                                 join c in db.SanPhams
                                 on b.MaSP equals c.MaSp
                                 where a.NgayXuat.Value.Year == tronnam && a.MaKho == MaKho && a.MaKH == MaKH
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
                        return View(hd);

                    }
                }
            }
        }
    }
}