using InventoryMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventoryMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _http;

        public DashboardController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var json = await _http.GetStringAsync("api/Reports");

            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            int GetInt(string name) =>
                root.TryGetProperty(name, out var p) && p.TryGetInt32(out var v) ? v : 0;

            decimal GetDecimal(string name) =>
                root.TryGetProperty(name, out var p) && p.TryGetDecimal(out var v) ? v : 0;

            var vm = new DashboardVM
            {
                TotalProducts = GetInt("totalProducts"),
                TotalSales = GetInt("totalSales"),
                TotalPurchases = GetInt("totalPurchases"),
                LowStock = GetInt("lowStock"),

                Revenue = GetDecimal("totalRevenue"),
                PurchaseCost = GetDecimal("totalPurchaseCost"),
                Profit = GetDecimal("profit")
            };

            return View(vm);
        }
    }
}
