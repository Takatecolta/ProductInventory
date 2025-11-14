// /Dtos/ProductWithStockDto.cs
namespace MySystem.Dtos
{
    public class ProductWithStockDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        // 在庫合計数
        public int Stock { get; set; }
    }
}
