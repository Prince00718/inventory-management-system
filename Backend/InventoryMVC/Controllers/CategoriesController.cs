using InventoryMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace InventoryMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _http;

        public CategoriesController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        // ===== INDEX =====
        public async Task<IActionResult> Index()
        {
            var categories = await _http.GetFromJsonAsync<List<CategoryVM>>("api/Categories");
            return View(categories ?? new List<CategoryVM>());
        }

        // ===== CREATE GET =====
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryVM());
        }

        // ===== CREATE POST =====
        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var res = await _http.PostAsJsonAsync("api/Categories", vm);

            if (res.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create category");
            return View(vm);
        }
    }
}
