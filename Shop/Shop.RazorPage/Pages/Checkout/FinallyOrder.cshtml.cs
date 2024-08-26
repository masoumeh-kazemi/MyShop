using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.OrderAgg;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.RazorPage.Pages.Checkout
{
    public class FinallyOrderModel : PageModel
    {
        private IOrderFacade _orderFacade;

        public FinallyOrderModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public OrderDto Order { get; set; }
        public async Task<IActionResult> OnGet(long orderId)
        {
            var order = await _orderFacade.GetById(orderId);
            if (order == null || order.UserId != User.GetUserId())
                return Redirect("/");

            Order = order;
            return Page();
        }
    }
}
