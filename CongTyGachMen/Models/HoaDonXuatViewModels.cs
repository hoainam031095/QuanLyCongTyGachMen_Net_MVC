using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CongTyGachMen.Models;

namespace CongTyGachMen.Models
{
    public class HoaDonXuatViewModels
    {
        public string MaHD { get; set; }
        public string MaSP { get; set; }
        public string LyDo { get; set; }
        public string TenKho { get; set; }
        public string TenNV { get; set; }
        public string TenKH { get; set; }
        public string TenSP { get; set; }
        public string NguoiCho { get; set; }
        public string BienXe { get; set; }
        public int SoLuong { set; get; }
        public int DonGia { set; get; }
        public DateTime? NgayXuat { set; get; }
        public int ThanhTien
        {
            get; set;
        }
    }
}