using Shop.Domain.SellerAgg;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.Query.Sellers.Inventories;

public static class SellerMapper
{
    public static SellerDto MapToDto(this Seller seller)
    {
        return new SellerDto()
        {
            CreationDate = seller.CreationDate,
            Id = seller.Id,
            UserId = seller.UserId,
            NationalCode = seller.NationalCode,
            ShopName = seller.ShopName,
            Status = seller.Status,
        };
    }
}