using MySystem.Dtos;
using ProductInventory.Models;
using ProductInventoryMVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProductInventoryMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Product CRUD
        //商品情報のリストを取得する
        public async Task<List<ProductWithStockDto>> GetProductsAsync()
        {
            // APIのDTOに合わせてデシリアライズ
            var products = await _httpClient.GetFromJsonAsync<List<ProductWithStockDto>>("/api/products");
            return products ?? new List<ProductWithStockDto>();
        }

        //商品情報を取得する
        public async Task<ProductInventory.Models.ProductModel> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductInventory.Models.ProductModel>($"/api/products/{id}");
        }

        //新しい商品を作成する
        public async Task<bool> CreateProductAsync(ProductInventory.Models.ProductModel product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/products", product);
            return response.IsSuccessStatusCode;
        }

        //商品情報を更新する
        public async Task<bool> UpdateProductAsync(int id, ProductInventory.Models.ProductModel product)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/products/{id}", product);
            return response.IsSuccessStatusCode;
        }

        //商品情報を削除する
        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");
            return response.IsSuccessStatusCode;
        }

        // Inventory CRUD
        //在庫一覧を取得する
        public async Task<List<Models.InventoryModel>> GetInventoriesAsync(int productId)
        {
            return await _httpClient.GetFromJsonAsync<List<Models.InventoryModel>>("/api/inventories");
        }

        ////商品IDに紐づいた在庫を取得する
        //public async Task<List<Models.InventoryModel>> GetInventoriesByProductAsync()
        //{
        //    return await _httpClient.GetFromJsonAsync<List<Models.InventoryModel>>($"/api/inventories/by-product/{productId}");
        //}


        //特定の在庫レコードを更新する
        public async Task<bool> UpdateInventoryAsync(int id, Models.InventoryModel inventory)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/inventories/{id}", inventory);
            return response.IsSuccessStatusCode;
        }
    }
}
