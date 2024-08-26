using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Domain.ProductAgg;

public class Product : AggregateRoot
{
    private Product()
    {

    }
    public Product(string title, string imageName, string description, long categoryId, long subCategoryId
        , long? secondarySubCategoryId, IProductDomainService domainService, string slug, SeoData seoData)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        //Guard(title, slug, description, domainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
        Images = new List<ProductImage>();
        Specifications = new List<ProductSpecification>();
    }
    public string Title { get; private set; }
    public string ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long? SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification> Specifications { get; private set; }


    public void Edit(string title, string description, long categoryId, long subCategoryId
        , long? secondarySubCategoryId, IProductDomainService domainService, string slug, SeoData seoData)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug;
        SeoData = seoData;
    }

    public void SetSpecification(List<ProductSpecification> specifications)
    {
        specifications.ForEach(f => f.ProductId = Id);
        Specifications = specifications;
    }

    public void SetImageName(string imageName)
    {
        ImageName = imageName;
    }


    public void AddImageGallery(ProductImage productImage)
    {
        productImage.ProductId = Id;
        Images.Add(productImage);
    }

    public void DeleteImageGallery(ProductImage productImage)
    {
        Images.Remove(productImage);
    }

    public void DeleteImageGallery(long productImageId)
    {
        var productImage = Images.FirstOrDefault(f => f.Id == productImageId);
        if (productImage == null)
        {
            throw new NullOrEmptyDomainDataException();
        }

        Images.Remove(productImage);
    }
}

