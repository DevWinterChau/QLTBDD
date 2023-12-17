using System.ComponentModel.DataAnnotations;

namespace ITShop.Models
{
    public class HangSanXuat
    {
        [Display(Name = "Mã hãng sản xuất")]
        public int ID { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên hãng sản xuất không được bỏ trống")]
        [Display(Name = "Tên hãng sản xuất")]
        public string TenHangSanXuat { get; set; }
        public ICollection<SanPham>? SanPham { get; set; }
    }
}
