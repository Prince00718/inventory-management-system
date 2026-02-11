using InventoryMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace InventoryMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _http;

        public ProductsController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var products = await _http.GetFromJsonAsync<List<ProductVM>>("api/Products");
            return View(products ?? new List<ProductVM>());
        }

        // ================= CREATE GET =================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
            ViewBag.Categories = categories ?? new List<CategoryVM>();

            return View(new ProductVM());
        }

        // ================= CREATE POST =================
        [HttpPost]
        public async Task<IActionResult> Create(ProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
                ViewBag.Categories = categories ?? new List<CategoryVM>();
                return View(vm);
            }

            var response = await _http.PostAsJsonAsync("api/Products", vm);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create product");

            var cats = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
            ViewBag.Categories = cats ?? new List<CategoryVM>();

            return View(vm);
        }

        // ================= EDIT GET =================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _http.GetFromJsonAsync<ProductVM>($"api/Products/{id}");

            if (product == null)
                return RedirectToAction("Index");

            var categories = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
            ViewBag.Categories = categories ?? new List<CategoryVM>();

            return View(product);
        }

        // ================= EDIT POST =================
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
                ViewBag.Categories = categories ?? new List<CategoryVM>();
                return View(vm);
            }

            var response = await _http.PutAsJsonAsync($"api/Products/{vm.Id}", vm);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update product");

            var cats = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
            ViewBag.Categories = cats ?? new List<CategoryVM>();

            return View(vm);
        }

        // ================= DELETE =================
        public async Task<IActionResult> Delete(int id)
        {
            await _http.DeleteAsync($"api/Products/{id}");
            return RedirectToAction("Index");
        }
    }
}
