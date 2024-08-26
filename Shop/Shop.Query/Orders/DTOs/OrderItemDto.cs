using System.Security.AccessControl;
using Common.Query;

namespace Shop.Query.Orders.DTOs;

public class OrderItemDto : BaseDto
{
    public int Price { get; set; }
    public int Count { get; set; }
    public string ShopName { get; set; }

    public string ProductTitle { get; set; }
    public string ProductImageName { get; set; }
    public string ProductSlug { get; set; }
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public long SellerId { get; set; }
    public int DiscountPercentage { get; set; }

    public int DiscountPrice
    {
        get
        {
            if (DiscountPercentage > 0)
            {
                var discountAmount = (DiscountPercentage * Price) / 100;
                var totalPriceWithDiscount = Price - discountAmount;
                return totalPriceWithDiscount;
            }
            else
            {
                return Price;
            }
        }
    }

    public int TotalPrice => DiscountPrice * Count;


}