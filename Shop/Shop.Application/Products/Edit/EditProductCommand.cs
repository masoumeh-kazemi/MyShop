using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Edit;

public class EditProductCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Slug { get; private set; }
    public IFormFile? ImageFile { get; private set; }
    public SeoData SeoData { get; private set; }
    public long CategoryId { get; private set; }

    public long SubCategoryId { get; private set; }
    public long? SecondarySubCategoryId { get; private set; }
    public Dictionary<string, string> Specifications { get; private set; }

    public EditProductCommand(long id, string title, string description, string slug, IFormFile? imageFile, SeoData seoData, long categoryId, long subCategoryId, long? secondarySubCategoryId, Dictionary<string, string> specifications)
    {
        Id = id;
        Title = title;
        Description = description;
        Slug = slug;
        ImageFile = imageFile;
        SeoData = seoData;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Specifications = specifications;
    }

}

public class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    private readonly IProductDomainService _domainService;
    public EditProductCommandHandler(IProductRepository productRepository, IFileService fileService, IProductDomainService domainService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
        _domainService = domainService;
    }
    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.Id);
        if (product == null)
        {
            return OperationResult.NotFound();
        }

        if (request.ImageFile != null)
        {
            var oldImage = Directories.GetProductImage(product.ImageName);
            _fileService.DeleteFile(Directories.ProductImages, oldImage);
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
            
            product.SetImageName(imageName);

        }


        var specifications = new List<ProductSpecification>();

        foreach (var item in request.Specifications)
        {
            specifications.Add(new ProductSpecification(item.Key, item.Value));
        }

        product.SetSpecification(specifications);

        product.Edit(request.Title, request.Description, request.CategoryId, request.SubCategoryId,
            request.SecondarySubCategoryId, _domainService, request.Slug, request.SeoData);

        await _productRepository.Save();
        return OperationResult.Success();
    }


}