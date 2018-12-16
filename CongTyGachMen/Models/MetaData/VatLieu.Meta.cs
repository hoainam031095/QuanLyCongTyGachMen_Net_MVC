using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CongTyGachMen.Models
{
    [MetadataTypeAttribute(typeof(VatLieuMetaData))]
    public partial class VatLieu
    {
        internal sealed class VatLieuMetaData
        {
            [Display(Name = "Mã vật liệu")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string MaVL { get; set; }
            [Display(Name = "Tên vật liệu ")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string TenVL { get; set; }
            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public Nullable<int> SoLuongTon { get; set; }
            [Display(Name = "Đơn vị tính")]
            [Required(ErrorMessage = "Chưa nhập dữ liệu cho trường này.")]
            public string DVT { get; set; }

        }
    }
}