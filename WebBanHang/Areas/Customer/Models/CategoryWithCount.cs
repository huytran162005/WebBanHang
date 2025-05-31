using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanHang.Areas.Customer.Models
{
    public class CategoryWithCount
    {
        public Category Category { get; set; }
        public int ProductCount { get; set; }
    }
}
