using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObjects;
using System.Diagnostics;

namespace Shop.Domain.OrderAgg;

public class Order : AggregateRoot
{
    private Order()
    {

    }
    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
    }
    public long UserId { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public OrderShippingMethod? ShippingMethod { get; set; }
    public OrderAddress? Address { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime LastUpdate { get; private set; } = DateTime.Now;
    public int TotalPrice {
        get
        {
            var totalPrice = Items.Sum(s => s.TotalPrice);
            if (ShippingMethod != null)
                totalPrice += ShippingMethod.ShippingCost;
            if (Discount != null)
                totalPrice -= Discount.DiscountAmount;
            return totalPrice;
        } }

    public int ItemCount => Items.Count;


    public void AddOrderItem(int count, int price, int? discount, long inventoryId)
    {
        var currentOrderItem = Items.FirstOrDefault(f => f.InventoryId == inventoryId);

        if (currentOrderItem != null)
        {
            currentOrderItem.AddCount(count);
        }
        else
        {
            var discountPrice = 0;
            if (discount > 0)
            {
                var discountAmount = (discount * price) / 100;
                 discountPrice = (int)(price - discountAmount);
                
            }
            else
            {
                discountPrice = price;
            }
            Items.Add(new OrderItem(count, price, discountPrice, inventoryId));

        }
    }

    public void DeleteOrderItem(long itemId)
    {
        var orderItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (orderItem == null)
        {
            throw new NullOrEmptyDomainDataException();
        }
        Items.Remove(orderItem);
    }

    public void IncreaseItemCount(long itemId,int count)
    {
        var item = Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
        {
            throw new NullOrEmptyDomainDataException();

        }
        item.IncreaseCount(count);

    }

    public void DecreaseItemCount(long itemId, int count)
    {
        var item = Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
        {
            throw new NullOrEmptyDomainDataException();
        }
        item.DecreaseCount(count);

    }

    public void SetAddress(OrderAddress orderAddress)
    {
        Address = orderAddress;
    }

    public void SetShippingMethod(OrderShippingMethod orderShippingMethod)
    {
        ShippingMethod = orderShippingMethod;
    }

    public void Finally()
    {
        Status = OrderStatus.Finally;
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
    }
}