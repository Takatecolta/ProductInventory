using Microsoft.AspNetCore.Mvc;
using ProductInventory.ViewModel;
using ProductInventoryMVC.Services;

namespace ProductInventoryMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiService _apiService;

        public ProductController(ApiService apiService)
        {
            _apiService = apiService;
        }
        //商品一覧を表示するためのメソッド
        public async Task<IActionResult> Index()
        {
            var  products = await _apiService.GetProductsAsync(); 
            var vm = new ProductListViewModel { Products = products }; return View(vm);
        }

        //新しい商品を作成するためのフォーム画面を返すメソッド
        public IActionResult Create()
        {
            return View();
        }

        //入力された情報を登録するメソッド
        [HttpPost]
        public async Task<IActionResult> Create(ProductInventory.Models.ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);
            await _apiService.CreateProductAsync(product); return RedirectToAction(nameof(Index));
        }
        
        //商品情報を更新するためのビューを表示するメソッド
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _apiService.GetProductAsync(id); 
            return View(product);
        }

        //商品情報を更新するメソッド
        [HttpPost]
        public async Task<IActionResult> Edit(int id,ProductInventory.Models.ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);
            await _apiService.UpdateProductAsync(id, product); return RedirectToAction(nameof(Index));
        }

        //商品情報を削除するメソッド
        public async Task<IActionResult> Delete(int id)
        {
            //在庫情報の確認
            List<Models.InventoryModel> hasInventories = await _apiService.GetInventoriesByProductAsync(id);

            if (hasInventories == null && hasInventories.Count == 0)
            {
                // 在庫がある場合は削除できないのでエラーメッセージを渡す
                TempData["ErrorMessage"] = "この商品は在庫データが存在するため削除できません。";
                return RedirectToAction(nameof(Index));
            }

            //在庫がなければ商品を削除
            await _apiService.DeleteProductAsync(id); return RedirectToAction(nameof(Index));
        }
    }
}