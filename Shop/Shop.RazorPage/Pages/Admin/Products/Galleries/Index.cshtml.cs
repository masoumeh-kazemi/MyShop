using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Galleries.Add;
using Shop.Application.Products.Galleries.Delete;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.Pages.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Pages.Admin.Products.Galleries
{
    public class IndexModel : BaseRazorPage
    {
        [BindProperty]
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }


        [BindProperty]
        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int Sequence { get; set; }

        public long ProductId { get; set; }
        public List<ProductImageDto> ProductImages { get; set; }
        private readonly IRenderViewToString _renderViewToString;

        private readonly IProductFacade _productFacade;

        public IndexModel(IProductFacade productFacade, IRenderViewToString renderViewToString)
        {
            _productFacade = productFacade;
            _renderViewToString = renderViewToString;
        }
        public async Task OnGet(long productId)
        {
            var product = await _productFacade.GetById(productId);
            ProductImages = product.Images;
            ProductId = productId;
        }


        public async Task<IActionResult> OnPostDeleteImageProduct(long productId, long imageId)
        {

            return await AjaxTryCatch(async () =>
            {
                var result = await _productFacade
                    .DeleteProductImage(new DeleteProductGalleryImageCommand(productId, imageId));

                return OperationResult.Success();
            }, checkModelState:false);
        }

        //public async Task<IActionResult> OnGetShowAddImageGallery()
        //{
            
        //    return await AjaxTryCatch(async () =>
        //    {
        //        var view = await _renderViewToString
        //            .RenderToStringAsync("_Add", new AddImageGalleryViewModel(), PageContext);

        //        return OperationResult<string>.Success(view);
        //    });

        //}

        public async Task<IActionResult> OnPost(long productId)
        {

            return await AjaxTryCatch(async () =>
            {
                var result = await _productFacade
                    .AddProductImage(new AddProductGalleryImageCommand(productId, ImageFile, Sequence));
                return result;
            }, checkModelState: false);
        }

    }
}
