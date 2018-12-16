using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(KhachHangMetaData))]
    public partial class KhachHang
    {
        internal sealed class KhachHangMetaData
        {
            private KhachHangMetaData() { }

            [Display(Name = "Mã nhà khách hàng")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string MaKH { get; set; }
            [Display(Name = "Tên nhà khách hàng")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string TenKH { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string DiaChi { get; set; }
            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string Phone { get; set; }
        }
    }
}