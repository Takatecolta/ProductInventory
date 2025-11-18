using Microsoft.AspNetCore.Mvc;
using MySystem.Dtos;
using ProductInventory.ViewModel;
using ProductInventoryMVC.Services;
using X.PagedList;
using X.PagedList.Extensions;

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
        public async Task<IActionResult> Index(int? page)
        {

            //１ページに表示する件数
            int pageSize = 10;

            //ページがnullなら1ページ目
            int pageNumber = page ?? 1;
            if (pageNumber < 1) pageNumber = 1;

            // APIから全商品のリストを取得
            ////商品ごとの在庫を取得
            var products = await _apiService.GetProductsAsync(); 

            //ページング処理
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            ProductListViewModel vm = new ProductListViewModel 
            { 
                Products = pagedProducts.ToList(),
                PageInfo = pagedProducts
            };
            
            return View(vm);
        }

        //新しい商品を作成するためのフォーム画面を返すメソッド
        public IActionResult Create()
        {
            return View();
        }


        //リアルタイム検索用のアクション
        [HttpGet]
        public async Task<IActionResult> Search(string search, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            // APIサービスで全件取得（searchが空の場合）
            List<ProductWithStockDto> products;
            if (string.IsNullOrWhiteSpace(search))
            {
                products = await _apiService.GetProductsAsync(); // 全件取得メソッド
            }
            else
            {
                products = await _apiService.SearchProductsAsync(search); // キーワードで絞り込み
            }

            // ページング対応
            var pagedList = products.ToPagedList(pageNumber, pageSize);

            // 部分ビューで返す場合
            return PartialView("Search", pagedList);
        }


        //入力された情報を登録するメソッド
        [HttpPost]
        public async Task<IActionResult> Create(ProductInventory.Models.ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);
            bool result = await _apiService.CreateProductAsync(product);
            if (!result)
            {
                ModelState.AddModelError("", "商品登録に失敗しました。");
                return View(product);
            }

            // 成功した場合は TempData にメッセージをセット
            TempData["SuccessMessage"] = "商品が登録されました！";

            return RedirectToAction(nameof(Index));
        }

        //商品情報を更新するためのビューを表示するメソッド
        public async Task<IActionResult> Edit(int id)
        {
            ProductDetailDto product = await _apiService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            ProductDetailViewModel vm = new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                TotalQuantity = product.TotalQuantity
            };

            return View(vm);
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var product = await _apiService.GetProductAsync(id); 
        //    return View(product);
        //}

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
            ////在庫情報の確認
            //List<Models.InventoryModel> hasInventories = await _apiService.GetInventoriesByProductAsync();

            //if (hasInventories == null && hasInventories.Count == 0)
            //{
            //    // 在庫がある場合は削除できないのでエラーメッセージを渡す
            //    TempData["ErrorMessage"] = "この商品は在庫データが存在するため削除できません。";
            //    return RedirectToAction(nameof(Index));
            //}

            //在庫がなければ商品を削除
            await _apiService.DeleteProductAsync(id); return RedirectToAction(nameof(Index));
        }
    }
}