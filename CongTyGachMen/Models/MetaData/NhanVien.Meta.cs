using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(NhanVienMetaData))]
    public partial class NhanVien
    {
        internal sealed class NhanVienMetaData
        {
            [Display(Name = "Mã")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string MaNV { get; set; }
            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string TenNV { get; set; }
            [Display(Name = "Chức vụ")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string Chucvu { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string Diachi { get; set; }
            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string Phone { get; set; }
            [Display(Name = "Mail")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string Email { get; set; }
            [Display(Name ="Ảnh")]
            public string Image { get; set; }
            [Display(Name = "Lương")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string Luong { get; set; }
        }
    }
}