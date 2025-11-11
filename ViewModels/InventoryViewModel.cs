using ProductInventory.Models;
using ProductInventoryMVC.Models;

namespace ProductInventory.ViewModel
{
    public class InventoryListViewModel
    {
        public List<InventoryModel> Inventories { get; set; }
    }
    public class InventoryDetailViewModel
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }

        // 表示用に Product 名だけ持たせる
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        // 日付表示用に文字列化
        public string UpdatedAtText => UpdatedAt.ToString("yyyy/MM/dd HH:mm");
        public DateTime UpdatedAt { get; set; }
    }
}
