using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(KhoMetaData))]
    public partial class Kho
    {
        internal sealed class KhoMetaData
        {
            private KhoMetaData() { }

            [Display(Name = "Mã kho")]
            [Required(ErrorMessage ="Bạn chưa nhập thông tin trường này")]
            public string MaKho { get; set; }
            [Display(Name = "Tên kho")]
            [Required(ErrorMessage = "Bạn chưa nhập thông tin trường này")]
            public string TenKho { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "Bạn chưa nhập thông tin trường này")]
            public string DiaChi { get; set; }
            [Display(Name = "Phone")]
            [Required(ErrorMessage = "Bạn chưa nhập thông tin trường này")]
            public string Phone { get; set; }
            [Display(Name = "Nhân viên")]
            public string MaNV { get; set; }
        }
    }
}