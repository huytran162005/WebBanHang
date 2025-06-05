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
            // Create view model instance
            var viewModel = new CategoryProductListVM();

            // Get category list with product counts
            viewModel.Categories = _db.Categories
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new CategoryListVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = _db.Products.Count(p => p.CategoryId == c.Id)
                })
                .ToList();

            // Only load products if a specific category is selected
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var selectedCategory = _db.Categories.FirstOrDefault(c => c.Id == categoryId.Value);
                if (selectedCategory != null)
                {
                    viewModel.SelectedCategoryName = selectedCategory.Name;

                    // Get products for the selected category
                    viewModel.Products = _db.Products
                        .Include(p => p.Category)
                        .Where(p => p.CategoryId == categoryId.Value)
                        .OrderBy(p => p.Name)
                        .ToList();
                }
            }
            else
            {
                // No category selected, don't fetch any products initially
                viewModel.Products = new List<Product>();
            }

            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetProductsByCategory(int categoryId)
        {
            // If categoryId is 0, don't return all products - return empty list
            if (categoryId == 0)
            {
                return Json(new List<object>());
            }

            var products = _db.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
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