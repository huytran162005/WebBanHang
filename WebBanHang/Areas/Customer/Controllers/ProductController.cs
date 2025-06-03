using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
