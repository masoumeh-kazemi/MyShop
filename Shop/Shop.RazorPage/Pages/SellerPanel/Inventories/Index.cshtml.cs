using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Sellers.Inventories.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.Pages.ViewModel;

namespace Shop.RazorPage.Pages.SellerPanel.Inventories
{
    public class IndexModel : BaseRazorPage
    {
        public List<InventoryDto> Inventories { get; set; }

        private readonly ISellerInventoryFacade _inventoryFacade;
        private readonly ISellerFacade _sellerFacade;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(ISellerInventoryFacade inventoryFacade, ISellerFacade sellerFacade, IRenderViewToString renderViewToString)
        {
            _inventoryFacade = inventoryFacade;
            _sellerFacade = sellerFacade;
            _renderViewToString = renderViewToString;
        }

        public async Task<IActionResult> OnGet()
        {
            var seller = await _sellerFacade.GetByUserId(User.GetUserId());
            if (seller == null)
                return RedirectToPage("/Auth/SellerRegister");

            var inventories = await _inventoryFacade.GetByList(seller.Id);
            Inventories = inventories;
            return Page();
        }

        public async Task<IActionResult> OnGetShowEditPage(long inventoryId)
        {
            return await AjaxTryCatch(async () =>
            {
                var inventory = await _inventoryFacade.GetById(inventoryId);

                var model = new EditSellerInventoryViewModel()
                {
                    Count = inventory.Count,
                    Price = inventory.Price,
                    //DiscountPercentage = inventory.DiscountPercentage,
                    ProductId = inventory.ProductId,
                    SellerId = inventory.SellerId,
                    InventoryId = inventory.Id
                };

                var view = await _renderViewToString
                    .RenderToStringAsync("_Edit", model, PageContext);

                return OperationResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostEditInventory(long inventoryId, long sellerId, EditSellerInventoryViewModel viewModel)
        {


            return await AjaxTryCatch(async () =>
            {
                var result = await _inventoryFacade.Edit(new EditSellerInventoryCommand(sellerId, inventoryId,
                    viewModel.Count, viewModel.Price, 0));
                return OperationResult.Success();

            });
        }
    }
}
