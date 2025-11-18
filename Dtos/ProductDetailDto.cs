public class ProductDetailDto
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    //public List<InventoryDto> Inventories { get; set; } = new();
    // 商品に紐づく在庫の合計
    public int TotalQuantity { get; set; }

}


public class InventoryDto
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

