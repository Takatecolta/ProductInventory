using Microsoft.AspNetCore.Mvc;
using ProductInventory.ViewModel;
using ProductInventoryMVC.Services;

namespace ProductInventoryMVC.Controllers
{
    public class InventoryAPIController : Controller
    {
        private readonly ApiService _apiService;
        public InventoryAPIController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var inventories = await _apiService.GetInventoriesAsync(); 
            var vm = new InventoryListViewModel { Inventories = inventories }; return View(vm);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var inventory = await _apiService.GetInventoriesByProductAsync(id); return View(inventory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductInventoryMVC.Models.InventoryModel inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            await _apiService.UpdateInventoryAsync(id, inventory); return RedirectToAction(nameof(Index));
        }
    }
}
