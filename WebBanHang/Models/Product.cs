using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(200, ErrorMessage = "Tên không được vượt quá 200 ký tự.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Giá không được để trống.")]
        [Range(1, 100000, ErrorMessage = "Giá phải nằm trong khoảng từ 1 đến 100000")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        //khai báo mối kết hợp 1-n
        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; } //khai báo mối kết hợp 1 - nhiều
        public string ImageUrl { get; set; }
    }
}
