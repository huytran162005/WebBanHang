using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebBanHang.Models;
using WebBanHang.Areas.Customer.Models;
using System.Collections.Generic;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? categoryId = null)
        {
            // Lấy danh sách category (strongly-typed)
            var categories = _db.Categories
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new CategoryListVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = _db.Products.Count(p => p.CategoryId == c.Id)
                })
                .ToList();

            // Lấy danh mục đang chọn (nếu có)
            Category selectedCategory = null;
            if (categoryId.HasValue && categoryId.Value > 0)
                selectedCategory = _db.Categories.FirstOrDefault(c => c.Id == categoryId.Value);

            // Query product list
            var productsQuery = _db.Products.Include(p => p.Category).AsQueryable();
            if (categoryId.HasValue && categoryId.Value > 0)
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);

            var products = productsQuery.OrderBy(p => p.CategoryId).ThenBy(p => p.Name).ToList();

            // Truyền viewmodel mạnh kiểu cho View
            var model = new WebBanHang.Areas.Customer.Models.ProductCategoryPageVM
            {
                Categories = categories,
                Products = products,
                SelectedCategoryId = categoryId ?? 0,
                SelectedCategoryName = selectedCategory?.Name ?? ""
            };

            return View(model);
        }
        [HttpGet]
        public JsonResult GetProductsByCategory(int categoryId)
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Where(p => categoryId == 0 || p.CategoryId == categoryId) // Nếu categoryId = 0, trả về tất cả sản phẩm
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    CategoryName = p.Category.Name
                })
                .ToList();

            return Json(products);
        }

        public IActionResult ByCategory(int id)
        {
            return RedirectToAction("Index", new { categoryId = id });
        }
    }
}