using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;

namespace Shop.RazorPage.Pages.Admin.Products
{
    public class IndexModel : BaseRazorFilter<ProductFilterParam>
    {

        public ProductFilterResult FilterResult { get; set; }

        private readonly ICategoryFacade _categoryFacade;
        private readonly IProductFacade _productFacade;

        public IndexModel(ICategoryFacade categoryFacade, IProductFacade productFacade)
        {
            _categoryFacade = categoryFacade;
            _productFacade = productFacade;
        }

        public async Task OnGet()
        {
            var product = await _productFacade.GetByFilter(FilterParam);
            FilterResult = product;
        }

        public void OnPost()
        {

        }


        public async Task<IActionResult> OnGetLoadChildCategories(long parentId)
        {
            var childCategories = await _categoryFacade.GetChildByParentId(parentId);
            var options = "<option value='0'>انتخاب کنید</option>";
            foreach (var item in childCategories)
            {
                options += $"<option value='{@item.Id}'>{item.Title}</option>";

            }

            return Content(options);
        }


        public async Task<IActionResult> OnPostDeleteProduct(long id)
        {
            return await AjaxTryCatch(async () =>
            {
                var result = await _productFacade.Delete(id);
                return OperationResult.Success();
            },checkModelState: false);
        }

    }
}
