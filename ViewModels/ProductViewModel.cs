using MySystem.Dtos;
using ProductInventory.Models;
using X.PagedList;

namespace ProductInventory.ViewModel
{
    public class ProductListViewModel
    {
        // APIのDTOをそのまま保持
        public List<ProductWithStockDto> Products { get; set; } = new();

        public IPagedList<ProductWithStockDto> PageInfo { get; set; }
    }
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //商品ごとの在庫数量
        public int TotalQuantity { get; set; } 
        //public List<InventoryDto> Inventories { get; set; } = new();
    }

}
