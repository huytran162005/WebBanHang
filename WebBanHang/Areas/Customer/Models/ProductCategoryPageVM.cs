    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Models
{
    public class ProductCategoryPageVM
    {
        public List<CategoryListVM> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
    }
        
}
