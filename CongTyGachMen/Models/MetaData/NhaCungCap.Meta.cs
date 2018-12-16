using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(NhaCungCapMetaData))]
    public partial class NhaCungCap
    {
        internal sealed class NhaCungCapMetaData
        {
            private NhaCungCapMetaData() { }

            [Display(Name ="Mã nhà cung cấp")]
            [Required(ErrorMessage ="Bạn chưa nhập dữ liệu trường này")]
            public string MaNCC { get; set; }
            [Display(Name = "Tên nhà cung cấp")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string TenNCC { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string DiaChi { get; set; }
            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "Bạn chưa nhập dữ liệu trường này")]
            public string Phone { get; set; }
        }
    }
}