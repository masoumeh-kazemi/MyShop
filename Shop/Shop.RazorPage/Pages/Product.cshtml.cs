using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Products.DTOs;
using Shop.Query.Sellers.Inventories.DTOs;

namespace Shop.RazorPage.Pages
{
    public class ProductModel : PageModel
    {
        
        public ProductDto Product { get; set; }
        public long SellerId { get; set; }
        public string? ShopName { get; set; }

        public List<InventoryDto>? Inventories { get; set; }
        private readonly IProductFacade _productFacade;
        private readonly ISellerInventoryFacade _sellerInventoryFacade;
        private readonly ISellerFacade _sellerFacade;
        public ProductModel(IProductFacade productFacade, ISellerInventoryFacade sellerInventoryFacade, ISellerFacade sellerFacade)
        {
            _productFacade = productFacade;
            _sellerInventoryFacade = sellerInventoryFacade;
            _sellerFacade = sellerFacade;
        }
        public async Task OnGet(string slug, long sellerId)
        {
            var seller = await _sellerFacade.GetById(sellerId);
            SellerId = sellerId;
            ShopName = seller.ShopName;
            var product = await _productFacade.GetBySlug(slug);
            var inventories = await _sellerInventoryFacade.GetListByProductId(product.Id);
            Product = product;
            Inventories = inventories;
        }
    }
}
