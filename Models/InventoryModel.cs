using System;

namespace ProductInventoryMVC.Models
{
    public class InventoryModel
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // 画面表示用
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static implicit operator Task<object>(InventoryModel v)
        {
            throw new NotImplementedException();
        }
    }
}
