using Common.Query;
using Shop.Domain.SellerAgg;

namespace Shop.Query.Sellers.Inventories.DTOs;

public class SellerDto : BaseDto
{
    public long UserId { get; set; }
    public string ShopName{ get; set; }
    public string NationalCode { get; set; }
    public SellerStatus Status { get; set; }
}