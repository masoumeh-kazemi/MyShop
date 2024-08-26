using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.OrderAgg;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.RazorPage.Pages.Profile.Orders
{
    public class IndexModel : PageModel
    {
        public OrderFilterResult Order { get; set; }
        private readonly IOrderFacade _orderFacade;

        public IndexModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }
        public async Task OnGet(OrderStatus? status=null)
        {
            var order = await _orderFacade.GetByFilter(new OrderFilterParam()
            {
                PageId = 1,
                Take = 100,
                UserId = User.GetUserId(),
                Status = status
            });

            Order = order;
        }
    }
}
