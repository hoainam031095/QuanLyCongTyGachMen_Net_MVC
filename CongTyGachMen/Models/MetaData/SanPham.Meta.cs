using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetaData))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetaData
        {
            [Display(Name = "Mã")]
            [Required(ErrorMessage ="Chưa nhập dữ liệu cho trường này.")]
            public string MaSp { get; set; }
            [Display(Name = "Tên")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string TenSp { get; set; }
            [Display(Name ="Ảnh")]
            public string Image { get; set; }
            [Display(Name = "Đơn vị tính")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string DVT { get; set; }
            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string SoLuongTon { get; set; }

        }
    }
}