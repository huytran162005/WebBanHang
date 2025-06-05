using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Models
{
    public class CategoryProductListVM
    {
        public List<CategoryListVM> Categories { get; set; }
        public List<Product> Products { get; set; }
        public string SelectedCategoryName { get; set; }
    }
}