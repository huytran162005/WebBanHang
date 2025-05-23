using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;

        // Fix: Corrected the constructor name to match the class name
        public HomeController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }

        public IActionResult Index()
        {
            var initialProducts = _db.Products
                .Include(p => p.Category)
                .Take(3)
                .ToList();

            ViewBag.TotalProductCount = _db.Products.Count();

            return View(initialProducts);
        }

        [HttpGet]
        public IActionResult LoadMoreProducts(int skip)
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Skip(skip)
                .Take(6)
                .ToList();

            return PartialView("_ProductCardsPartial", products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
