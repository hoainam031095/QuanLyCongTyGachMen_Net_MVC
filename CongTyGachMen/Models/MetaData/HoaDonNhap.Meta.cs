using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(HoaDonNhapMetaData))]
    public partial class HoaDonNhap
    {
        internal sealed class HoaDonNhapMetaData
        {
            private HoaDonNhapMetaData() { }

            [Display(Name="Hóa Đơn")]
            [Required(ErrorMessage ="Bạn chưa nhập dữ liệu cho trường này")]
            public string MaHD { get; set; }
            [Display(Name = "Lý do")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu cho trường này")]
            public string LyDo { get; set; }
            [Display(Name = "Kho")]
            public string MaKho { get; set; }
            [Display(Name = "Nhân viên")]
            public string MaNV { get; set; }
            [Display(Name = "Nhà cung cấp")]
            public string MaNCC { get; set; }
            [Display(Name = "Người chở")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu cho trường này")]
            public string NguoiCho { get; set; }
            [Display(Name = "Biển xe")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu cho trường này")]
            public string BienXe { get; set; }
            [Display(Name = "Ngày nhập")]
            public Nullable<System.DateTime> NgayNhap { get; set; }
        }
    }
}