using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.RazorPage.Pages.Profile.Orders
{
    public class ShowModel : PageModel
    {
        public OrderDto Order { get; set; }
        private readonly IOrderFacade _orderFacade;

        public ShowModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }
        public async Task OnGet(long id)
        {
            var order = await _orderFacade.GetById(id);
            
            Order = order;
        }
    }
}
