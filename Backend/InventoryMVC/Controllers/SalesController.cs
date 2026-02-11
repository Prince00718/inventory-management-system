using InventoryMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace InventoryMVC.Controllers
{
    public class SalesController : Controller
    {
        private readonly HttpClient _http;

        public SalesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        // ===== INDEX =====
        public async Task<IActionResult> Index()
        {
            var sales = await _http.GetFromJsonAsync<List<SaleVM>>("api/Sales");
            return View(sales ?? new List<SaleVM>());
        }


        // ===== CREATE GET =====
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
            ViewBag.Products = products ?? new List<ProductVM>();

            return View(new SaleVM());
        }

        // ===== CREATE POST =====
        [HttpPost]
        public async Task<IActionResult> Create(SaleVM vm)
        {
            if (!ModelState.IsValid)
            {
                var products = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
                ViewBag.Products = products ?? new List<ProductVM>();
                return View(vm);
            }

            var res = await _http.PostAsJsonAsync("api/Sales", vm);

            if (res.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create sale");

            var prods = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
            ViewBag.Products = prods ?? new List<ProductVM>();

            return View(vm);
        }
    }
}
