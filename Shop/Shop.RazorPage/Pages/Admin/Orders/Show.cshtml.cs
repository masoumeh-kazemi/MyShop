using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders.ChangeStatus;
using Shop.Domain.OrderAgg;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Orders
{
    public class ShowModel : BaseRazorPage
    {
        public OrderDto Order { get; set; }
        private readonly IOrderFacade _orderFacade;
        public ShowModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }
        public async Task<IActionResult> OnGet(long id)
        {
            var order = await _orderFacade.GetById(id);
            
            if (order == null)
                return RedirectToPage("Index");

            Order = order;
            return Page();
        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _orderFacade.SendOrder(new SendOrderCommand(id, OrderStatus.Shipping));
            return RedirectAndShowAlert(result, RedirectToPage("Show", new { id }));
        }


    }
}
