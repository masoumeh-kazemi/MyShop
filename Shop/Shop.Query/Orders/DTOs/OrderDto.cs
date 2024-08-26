using Common.Query;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Query.Orders.DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public OrderAddressDto? Address { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public OrderShippingMethod? ShippingMethod { get; set; }
    public OrderStatus Status { get; set; }
    
    public int? Discount { get; set; }
    public string UserFullName { get; set; }
    public DateTime LastUpdate { get; set; }
    public int TotalPrice { get; set; }
}