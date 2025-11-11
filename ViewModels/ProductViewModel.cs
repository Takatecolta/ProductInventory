using ProductInventory.Models;

namespace ProductInventory.ViewModel
{
    public class ProductListViewModel
    {
        public List<ProductModel> Products { get; set; }
    }
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string DisplayPrice => $"¥{UnitPrice:N0}";
            public decimal UnitPrice { get; set; }
            public string CreatedAtText => CreatedAt.ToString("yyyy/MM/dd");
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
    }
}
