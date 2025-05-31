using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebBanHang.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using WebBanHang.Areas.Customer.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;

        public ProductsController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }

        /// <summary>
        /// Hiển thị trang sản phẩm với 6 sản phẩm đầu tiên
        /// </summary>
        public IActionResult Index()
        {
            var initialProducts = _db.Products
                .Include(p => p.Category)
                .Take(6)
                .ToList();

            ViewBag.TotalProductCount = _db.Products.Count();

            return View(initialProducts);
        }

        /// <summary>
        /// API để tải thêm sản phẩm khi người dùng nhấn nút "Xem thêm"
        /// </summary>
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ProductsByCategory()
        {
            // Lấy tất cả danh mục kèm số lượng sản phẩm
            var categories = _db.Categories
                .Select(c => new CategoryWithCount
                {
                    Category = c,
                    ProductCount = _db.Products.Count(p => p.CategoryId == c.Id)
                })
                .OrderBy(x => x.Category.DisplayOrder)
                .ToList();

            // Lấy danh mục đầu tiên để hiển thị sản phẩm mặc định
            var firstCategory = categories.FirstOrDefault()?.Category;

            // Lấy sản phẩm của danh mục đầu tiên
            var products = new List<Product>();

            if (firstCategory != null)
            {
                products = _db.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == firstCategory.Id)
                    .ToList();

                ViewBag.SelectedCategoryName = firstCategory.Name;
                ViewBag.SelectedCategoryId = firstCategory.Id;
            }

            ViewBag.Categories = categories;

            return View(products);
        }

        public IActionResult GetProductsByCategory(int categoryId)
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            var categoryName = _db.Categories
                .FirstOrDefault(c => c.Id == categoryId)?.Name;

            ViewBag.SelectedCategoryName = categoryName;
            ViewBag.SelectedCategoryId = categoryId;

            // Nếu là AJAX request, trả về partial view
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductListPartial", products);
            }

            // Lấy tất cả danh mục kèm số lượng sản phẩm cho view đầy đủ
            ViewBag.Categories = _db.Categories
                .Select(c => new CategoryWithCount
                {
                    Category = c,
                    ProductCount = _db.Products.Count(p => p.CategoryId == c.Id)
                })
                .OrderBy(x => x.Category.DisplayOrder)
                .ToList();

            return View("ProductsByCategory", products);
        }
    }
}