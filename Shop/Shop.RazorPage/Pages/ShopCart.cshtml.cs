using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders.Create;
using Shop.Application.Orders.DecreaseItem;
using Shop.Application.Orders.Delete;
using Shop.Application.Orders.IncreaseItem;
using Shop.Domain.OrderAgg;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;
using Shop.RazorPage.Pages.Infrastructure.CookieUtils;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages
{
    public class ShopCartModel : BaseRazorPage
    {
        public OrderDto? CurrentOrder { get; set; }
        private readonly IOrderFacade _orderFacade;
        private readonly ShopCartCookieManager _cartCookieManager;
        public ShopCartModel(IOrderFacade orderFacade, ShopCartCookieManager cartCookieManager)
        {
            _orderFacade = orderFacade;
            _cartCookieManager = cartCookieManager;
        }
        public async Task OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentOrder = await _orderFacade.GetCurrent(User.GetUserId());
                if(currentOrder==null)
                    return;
                CurrentOrder = currentOrder;
            }
            else
            {
                var currentOrder = _cartCookieManager.GetShopCart();
                CurrentOrder = currentOrder;
            }

        }

        public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
        {

            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {
                    var currentOrder = await _orderFacade.GetCurrent(User.GetUserId());
                    var result = await _orderFacade.Create(new CreateOrderCommand(User.GetUserId(), count, inventoryId));
                    return result;
                });

            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _cartCookieManager.AddItem(inventoryId, count);
                    return result;
                });
            }
        }

        public async Task<IActionResult> OnPostIncreaseItemCount(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = _orderFacade.IncreaseItem(new IncreaseOrderItemCommand(1, id, User.GetUserId()));
                    return await result;
                });

            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                     _cartCookieManager.Increase(id);
                    return OperationResult.Success();

                });
            }
        }
        public async Task<IActionResult> OnPostDecreaseItemCount(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {

                    var result = _orderFacade.DecreaseItem(new DecreaseOrderItemCommand(1, id, User.GetUserId()));
                    return await result;
                });
            }

            else
            {
                return await AjaxTryCatch(async () =>
                {
                    _cartCookieManager.Decrease(id);
                    return OperationResult.Success();
                });
            }


        }

        public async Task<IActionResult> OnPostDeleteItem(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await AjaxTryCatch(async () =>
                {
                    var result = await _orderFacade
                        .DeleteItem(new DeleteOrderItemCommand(id, User.GetUserId()));
                    return result;
                });

            }
            else
            {
                return await AjaxTryCatch(async () =>
                {
                    _cartCookieManager.DeleteOrderItem(id);

                    return OperationResult.Success();

                });
            }
        }

        public async Task<IActionResult> OnGetShopCartDetail()
        {
            OrderDto? order = new();
            if (User.Identity.IsAuthenticated)
            {
                order = await _orderFacade.GetCurrent(User.GetUserId());
            }
            else
            {
                order = _cartCookieManager.GetShopCart();
            }

            return new ObjectResult(new
            {
                items = order?.OrderItems,
                count = order?.OrderItems.Sum(s => s.Count),
                price = $"{order?.OrderItems.Sum(s => s.TotalPrice):#,0} تومان"
            });
        }
    }
}
