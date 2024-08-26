using Common.Query;

namespace Shop.Query.Sellers.Inventories.DTOs;

public class InventoryDto : BaseDto
{
    public string ProductTitle { get; set; }
    public string ProductImageName { get; set; }
    public string ShopName { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public long ProductId { get; set; }
    public long SellerId { get; set; }
    public int TotalPrice { get
        {
            var total = Price;
            if (DiscountPercentage > 0)
            {
                var discount = (DiscountPercentage * Price) / 100;
                total -= discount;

            }
            return total;
        }

    }

}

