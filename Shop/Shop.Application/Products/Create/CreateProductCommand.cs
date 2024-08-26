using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Shop.Application.Products.Create;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create;

public class CreateProductCommand : IBaseCommand
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public SeoData SeoData { get; private set; }
    public IFormFile ImageFile { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long? SecondarySubCategoryId { get; private set; }
    public Dictionary<string, string> Specifications { get; private set; }

    public CreateProductCommand(string title, string slug, string description, SeoData seoData, IFormFile imageFile, long categoryId, long subCategoryId, long? secondarySubCategoryId, Dictionary<string, string> specifications)
    {
        Title = title;
        Slug = slug;
        Description = description;
        SeoData = seoData;
        ImageFile = imageFile;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Specifications = specifications;
    }
}

public class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _productDomainService;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(IProductRepository productRepository, IProductDomainService productDomainService, IFileService fileService)
    {
        _productRepository = productRepository;
        _productDomainService = productDomainService;
        _fileService = fileService;
    }


    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
        var product = new Product(request.Title, imageName, request.Description, request.CategoryId, request.SubCategoryId,
            request.SecondarySubCategoryId, _productDomainService, request.Slug, request.SeoData);
        
        var specifications = new List<ProductSpecification>();

        foreach (var item in request.Specifications)
        {
            specifications.Add(new ProductSpecification(item.Key, item.Value));
        }

        product.SetSpecification(specifications);

        _productRepository.Add(product);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}