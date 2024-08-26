using Dapper;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders;

public static class OrderMapper
{

    public static OrderDto MapToDto(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            UserId = order.UserId,
            ShippingMethod = order.ShippingMethod,
            Status = order.Status,
            Address = order.Address != null ? order.Address.MapToOrderAddress() : null,
            LastUpdate = order.LastUpdate,
            TotalPrice = order.TotalPrice
        };
    }

    public static OrderAddressDto? MapToOrderAddress(this OrderAddress orderAddress)
    {
        return new OrderAddressDto()
        {
            City = orderAddress.City,
            CreationDate = orderAddress.CreationDate,
            Id = orderAddress.Id,
            Family = orderAddress.Family,
            Name = orderAddress.Name,
            NationalCode = orderAddress.NationalCode,
            PhoneNumber = orderAddress.PhoneNumber,
            PostalAddress = orderAddress.PostalAddress,
            PostalCode = orderAddress.PostalCode,
            Shire = orderAddress.Shire,
        };
    }

    public static async Task GetOrderItem(this OrderDto order, DapperContext dapperContext)
    {
        var connection = dapperContext.CreateConnection();
        var sql = $@"SELECT o.Id, s.ShopName,s.Id as SellerId ,o.OrderId,o.InventoryId,o.Count,o.price, i.DiscountPercentage,
                     p.Title as ProductTitle , p.Slug as ProductSlug ,
                     p.ImageName as ProductImageName
        FROM {dapperContext.OrderItems} As o 
        INNER JOIN {dapperContext.SellerInventories}  i ON i.Id = o.InventoryId
        INNER JOIN {dapperContext.Sellers}  s ON i.SellerId = s.Id
        INNER JOIN {dapperContext.Products}  p ON i.ProductId=p.Id
        where o.OrderId=@orderId";

        var result = await connection.QueryAsync<OrderItemDto>(sql, new { orderId = order.Id });
        order.OrderItems = result.ToList();

        //using var connection = dapperContext.CreateConnection();
        //var sql = @$"SELECT o.Id, s.ShopName ,o.OrderId,o.InventoryId,o.Count,o.price,
        //                  p.Title as ProductTitle , p.Slug as ProductSlug ,
        //                  p.ImageName as ProductImageName
        //            FROM {dapperContext.OrderItems} o
        //            Inner Join {dapperContext.SellerInventories} i on i.Id = o.InventoryId
        //            Inner Join {dapperContext.Products} p on i.ProductId=p.Id
        //            Inner Join {dapperContext.Sellers} s on i.SellerId=s.Id
        //            where o.OrderId=@orderId";

        //var result = await connection
        //    .QueryAsync<OrderItemDto>(sql, new { orderId = order.Id });
        //order.OrderItems = result.ToList();
    }

    public static OrderFilterData MapToFilterData(this Order order, ShopContext shopContext)
    {
        var userFullName = shopContext.Users
            .Where(r => r.Id == order.UserId)
            .Select(u => $"{u.Name} {u.Family}")
            .First();
        return new OrderFilterData()
        {
            Status = order.Status,
            Id = order.Id,
            CreationDate = order.CreationDate,
            City = order.Address?.City,
            ShippingType = order.ShippingMethod?.ShippingType,
            Shire = order.Address?.Shire,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice,
            UserFullName = userFullName,
            UserId = order.UserId

        };
    }
}