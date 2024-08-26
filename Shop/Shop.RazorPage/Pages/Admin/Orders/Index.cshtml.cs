using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.Pages.Infrastructure.Utils;

namespace Shop.RazorPage.Pages.Admin.Orders
{
    public class IndexModel : BaseRazorFilter<OrderFilterParam>
    {
        public OrderFilterResult  FilterResult { get; set; }
        private readonly IOrderFacade _orderFacade;

        public IndexModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }
        public async Task OnGet(string? startDate, string? endDate, int pageId=1)
        {
            if (string.IsNullOrWhiteSpace(startDate) == false)
            {
                FilterParam.StartDate = startDate.ToMiladi();
            }

            if (string.IsNullOrWhiteSpace(endDate) == false)
            {
                FilterParam.EndDate = endDate.ToMiladi();
            }

            FilterParam.Take = 5;
            var filterResult = await _orderFacade.GetByFilter(FilterParam);
            FilterResult = filterResult;
        }
    }
}
