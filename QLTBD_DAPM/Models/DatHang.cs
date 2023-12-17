using System.ComponentModel.DataAnnotations;

namespace ITShop.Models
{
    public class DatHang
    {
        [Display(Name = "Mã đặt hàng")]
        public int ID { get; set; }

        [Display(Name = "Người dùng")]
        [Required(ErrorMessage = "Người dùng không được bỏ trống")]
        public int NguoiDungID { get; set; }

        [Display(Name = "Tình trạng")]
        [Required(ErrorMessage = "Tình trạng không được bỏ trống")]
        public int TinhTrangID { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại giao hàng")]
        [Required(ErrorMessage = "Điện thoại giao hàng không được bỏ trống")]
        public string DienThoaiGiaoHang { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ giao hàng")]
        [Required(ErrorMessage = "Địa chỉ giao hàng không được bỏ trống")]
        public string DiaChiGiaoHang { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        [Required(ErrorMessage = "Ngay đặt hàng không được bỏ trống")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayDatHang { get; set; }
        public ICollection<DatHang_ChiTiet>? DatHang_ChiTiet { get; set; }
        public NguoiDung? NguoiDung { get; set; }
        public TinhTrang? TinhTrang { get; set; }

    }
}
