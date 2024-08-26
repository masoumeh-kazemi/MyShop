
using Common.Application;
using CookieManager;
using Microsoft.AspNetCore.Http;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Infrastructure.Migrations;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Orders.DTOs;

namespace Shop.RazorPage.Pages.Infrastructure.CookieUtils;

public class ShopCartCookieManager
{
    private readonly ICookieManager _cookieManager;
    private const string CookieShopCartName = "shop-cart";
    private readonly IProductFacade _productFacade;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;

    public ShopCartCookieManager(ICookieManager cookieManager, IProductFacade productFacade, ISellerInventoryFacade sellerInventoryFacade)
    {
        _cookieManager = cookieManager;
        _productFacade = productFacade;
        _sellerInventoryFacade = sellerInventoryFacade;
    }


    public void DeleteShopCart()
    {
        _cookieManager.Remove(CookieShopCartName);
    }

    public OrderDto? GetShopCart()
    {
        return _cookieManager.Get<OrderDto>(CookieShopCartName);
    }

    public async Task<OperationResult> AddItem(long inventoryId, int count)
    {
        var shopCart = GetShopCart();
        var inventory = await _sellerInventoryFacade.GetById(inventoryId);
        var product = await _productFacade.GetById(inventory.ProductId);
        //var discount = inventory.DiscountPercentage;
        //var price = inventory.Price;
        //if (discount > 0)
        //{
        //    var discountAmount = (discount * price) / 100;
        //    price = (int)(price - discountAmount);
        //}
        //else
        //{
        //    price = price;
        //}
        if (shopCart == null)
        {
            var order = new OrderDto()
            {
                Address = null,
                CreationDate = DateTime.Now,
                Discount = null,
                Id = 1,
                UserId = 1,
                UserFullName = "",
                Status = OrderStatus.Pending,
                
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto()
                    {
                        DiscountPercentage = inventory.DiscountPercentage,
                        Price = inventory.Price,
                        Count = count,
                        ProductImageName = inventory.ProductImageName,
                        ShopName = inventory.ShopName,
                        CreationDate = DateTime.Now,
                        ProductTitle = inventory.ProductTitle,
                        InventoryId = inventoryId,
                        SellerId = inventory.SellerId,
                        OrderId = 1,
                        Id = GenerateId(),
                        ProductSlug = product!.Slug
                    }
                }
         

            };
            SetCookie(order);
        }
        else
        {
            if (shopCart.OrderItems.Any(f => f.InventoryId == inventoryId))
            {
                var item = shopCart.OrderItems.First(f => f.InventoryId == inventoryId);
                if (inventory.Count >= item.Count + count)
                {
                    item.Count += count;
                }
                else
                {
                    return OperationResult.Error("تعداد موجودی فروشنده کمتر از تعداد درخواستی است");
                }
            }


            else
            {
                var newItem = new OrderItemDto()
                {
                    Price = inventory.Price,
                    Count = count,
                    ProductImageName = inventory.ProductImageName,
                    ShopName = inventory.ShopName,
                    CreationDate = DateTime.Now,
                    ProductTitle = inventory.ProductTitle,
                    InventoryId = inventoryId,
                    OrderId = 1,
                    Id = GenerateId(),
                    ProductSlug = product!.Slug
                };
                shopCart.OrderItems.Add(newItem);
            }

            SetCookie(shopCart);
            return OperationResult.Success();
        }
        return OperationResult.Success();
    }

    public void Increase(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;

        var item = shopCart.OrderItems.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
            return;

        item.Count += 1;
        SetCookie(shopCart);
    }

    public void Decrease(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;
        var item = shopCart.OrderItems.FirstOrDefault(f => f.Id == itemId);

        if (item == null || item.Count == 1)
            return;
        item.Count -= 1;
        SetCookie(shopCart);

    }

    public void DeleteOrderItem(long itemId)
    {
        var shopCart = GetShopCart();
        if (shopCart == null)
            return;

        var item = shopCart.OrderItems.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
            return;

        shopCart.OrderItems.Remove(item);
        //if(shopCart.OrderItems.Count == 0)
        //    DeleteShopCart();

        SetCookie(shopCart);
    }
    private void SetCookie(OrderDto order)
    {
        _cookieManager.Set(CookieShopCartName, order, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
            Secure = true
        });
    }

    private long GenerateId()
    {
        var random = new Random();
        var number = random.Next(0, 10000) * 6 ^ 2 + random.Next(6, 1000000);
        return number;
    }
}