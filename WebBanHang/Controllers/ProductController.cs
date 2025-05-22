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

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }

        //Hiển thị danh sách sản phẩm có phân trang
        public IActionResult Index(int page = 1)
        {
            int pageSize = 7;
            int offset = 2;

            var totalItems = _db.Products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var products = _db.Products
                              .Include(x => x.Category)
                              .OrderBy(p => p.CategoryId)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.from = Math.Max(1, page - offset);
            ViewBag.to = Math.Min(totalPages, page + offset);

            return View(products);
        }

        //Hiển thị form thêm sản phẩm mới
        public IActionResult Add()
        {
            //truyền danh sách thể loại cho View để sinh ra điều khiển DropDownList
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        //Xử lý thêm sản phẩm
        [HttpPost]
        public IActionResult Add(Product product, IFormFile ImageUrl)
        {
            if (ModelState.IsValid) //kiểm tra hợp lệ dữ liệu
            {
                if (ImageUrl != null)
                {
                    //xu ly upload và lưu ảnh đại diện
                    product.ImageUrl = SaveImage(ImageUrl);
                }
                //thêm product vào table Product
                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        //Xử lý cập nhật sản phẩm
        [HttpPost]
        public IActionResult Update(Product product, IFormFile ImageUrl)
        {
            if (ModelState.IsValid) //kiểm tra hợp lệ dữ liệu
            {
                var existingProduct = _db.Products.Find(product.Id);
                if (ImageUrl != null)
                {
                    //xử lý upload và lưu ảnh đại diện mới
                    product.ImageUrl = SaveImage(ImageUrl);
                    //xóa ảnh cũ (nếu có)
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldFilePath = Path.Combine(_hosting.WebRootPath, existingProduct.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                else
                {
                    product.ImageUrl = existingProduct.ImageUrl;
                }
                //cập nhật product vào table Product
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ImageUrl = product.ImageUrl;
                _db.SaveChanges();
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        //Hiển thị form cập nhật sản phẩm
        public IActionResult Update(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            //truyền danh sách thể loại cho View để sinh ra điều khiển DropDownList
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(product);
        }

        private string SaveImage(IFormFile image)
        {
            //đặt lại tên file cần lưu
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //lấy đường dẫn lưu trữ wwwroot trên server
            var path = Path.Combine(_hosting.WebRootPath, @"images/products");
            var saveFile = Path.Combine(path, filename);
            using (var filestream = new FileStream(saveFile, FileMode.Create))
            {
                image.CopyTo(filestream);
            }
            return @"images/products/" + filename;
        }

        //Hiển thị form xác nhận xóa sản phẩm
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            // xoá hình cũ của sản phẩm
            if (!String.IsNullOrEmpty(product.ImageUrl))
            {
                var oldFilePath = Path.Combine(_hosting.WebRootPath, product.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            // xóa sản phẩm khỏi CSDL
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["success"] = "Xóa sản phẩm thành công";
            //chuyển đến trang index
            return RedirectToAction("Index");
        }
    }
}
