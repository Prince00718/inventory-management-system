using InventoryMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace InventoryMVC.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly HttpClient _http;

        public PurchasesController(IHttpClientFactory factory)
        {
            // use named client from Program.cs
            _http = factory.CreateClient("api");
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var data = await _http.GetFromJsonAsync<List<PurchaseVM>>("api/Purchases");
            return View(data ?? new List<PurchaseVM>());
        }

        // ================= CREATE GET =================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
            ViewBag.Products = products ?? new List<ProductVM>();

            return View(new PurchaseVM());
        }

        // ================= CREATE POST =================
        [HttpPost]
        public async Task<IActionResult> Create(PurchaseVM vm)
        {
            if (!ModelState.IsValid)
            {
                var products = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
                ViewBag.Products = products ?? new List<ProductVM>();
                return View(vm);
            }

            var res = await _http.PostAsJsonAsync("api/Purchases", vm);

            if (res.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create purchase");

            var prods = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
            ViewBag.Products = prods ?? new List<ProductVM>();

            return View(vm);
        }
    }
}
