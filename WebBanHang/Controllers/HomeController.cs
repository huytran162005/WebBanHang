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

        /// <summary>
        /// Hàm khởi tạo của HomeController
        /// </summary>
        /// <param name="db">Đối tượng ApplicationDbContext để tương tác với cơ sở dữ liệu</param>
        /// <param name="hosting">IWebHostEnvironment để truy cập thông tin môi trường máy chủ web</param>
        public HomeController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }

        /// <summary>
        /// Hiển thị trang chủ với 3 sản phẩm đầu tiên
        /// </summary>
        /// <returns>Trả về View chính với danh sách 3 sản phẩm để hiển thị ban đầu</returns>
        public IActionResult Index()
        {
            // Lấy 3 sản phẩm đầu tiên từ cơ sở dữ liệu, bao gồm cả thông tin danh mục
            var initialProducts = _db.Products
                .Include(p => p.Category)
                .Take(3)
                .ToList();

            // Lưu tổng số sản phẩm vào ViewBag để sử dụng trong View cho nút "Xem thêm"
            ViewBag.TotalProductCount = _db.Products.Count();

            return View(initialProducts);
        }

        /// <summary>
        /// API để tải thêm sản phẩm khi người dùng nhấn nút "Xem thêm"
        /// </summary>
        /// <param name="skip">Số sản phẩm cần bỏ qua (sản phẩm đã hiển thị)</param>
        /// <returns>Trả về PartialView chứa danh sách 6 sản phẩm tiếp theo</returns>
        [HttpGet]
        public IActionResult LoadMoreProducts(int skip)
        {
            // Lấy 6 sản phẩm tiếp theo, bỏ qua số lượng sản phẩm đã hiển thị
            var products = _db.Products
                .Include(p => p.Category)
                .Skip(skip)
                .Take(6)
                .ToList();

            // Trả về PartialView để thêm vào trang hiện tại qua AJAX
            return PartialView("_ProductCardsPartial", products);
        }

        /// <summary>
        /// Hiển thị trang chính sách bảo mật
        /// </summary>
        /// <returns>Trả về trang Privacy</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Xử lý và hiển thị trang lỗi khi có ngoại lệ xảy ra
        /// </summary>
        /// <returns>Trả về trang Error với thông tin lỗi</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Tạo đối tượng ErrorViewModel với ID của yêu cầu hiện tại để theo dõi lỗi
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
