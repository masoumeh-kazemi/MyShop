using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {

        public OrderItem(int count, int price, int discountPrice, long inventoryId)
        {
            Count = count;
            Price = price;
            DiscountPrice = discountPrice;
            InventoryId = inventoryId;
        }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public long InventoryId { get; private set; }
        public int TotalPrice => Price * Count;

        public int DiscountPrice { get; set; }


        public void AddCount(int count)
        {
            Count += count;
        }

        public void IncreaseCount(int count)
        {
            Count += count;
        }

        public void DecreaseCount(int count)
        {
            Count -= count;
        }
    }
}
