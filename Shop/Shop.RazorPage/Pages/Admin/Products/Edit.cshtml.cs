using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products.Edit;
using Shop.Presentation.Facade.Products;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.Admin.Products
{
    [BindProperties]
    public class EditModel : BaseRazorPage
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر محصول")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [Display(Name = "دسته بندی اول")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long CategoryId { get; set; }

        [Display(Name = "زیردسته اول")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long SubCategoryId { get; set; }

        [Display(Name = "زیردسته دوم")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public long? SecondarySubCategoryId { get; set; }

        [Display(Name = "slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }

        public SeoDataViewModel SeoData { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }

        private readonly IProductFacade _productFacade;

        public EditModel(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public async Task OnGet(long id)
        {
            var product = await _productFacade.GetById(id);
            Title = product.Title;
            Slug = product.Slug;
            Description = product.Description;
            CategoryId = product.CategoryId;
            SubCategoryId = product.SubCategoryId;
            SecondarySubCategoryId = product.SecondarySubCategoryId;
            SeoData = SeoDataViewModel.MapSeoDataToViewModel(product.SeoData);

        }

        public async Task<IActionResult> OnPost(long id)
        {
            var result = await _productFacade.Edit(new EditProductCommand(id, Title, Description,
                Slug, ImageFile, SeoDataViewModel.MapViewModelToSeoData(SeoData), CategoryId, SubCategoryId,
                SecondarySubCategoryId, SetSpecifications()));


            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

        private Dictionary<string, string> SetSpecifications()
        {
            var specifications = new Dictionary<string, string>();
            for (int i = 0; i < Keys.Count; i++)
            {
                specifications.Add(Keys[i], Values[i]);
            }

            return specifications;
        }
    }
}
