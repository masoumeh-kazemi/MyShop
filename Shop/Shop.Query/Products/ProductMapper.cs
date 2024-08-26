using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories;
using Shop.Query.Categories.DTOs;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products;

public static class ProductMapper 
{
    public static ProductDto MapToDto(this Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Title = product.Title,
            Slug = product.Slug,
            Description = product.Description,
            ImageName = product.ImageName,
            SeoData = product.SeoData,
            CategoryId = product.CategoryId,
            SubCategoryId = product.SubCategoryId,
            SecondarySubCategoryId = product.SecondarySubCategoryId,
            Images = MapToProductImageDto(product.Images),
            Specifications = product.Specifications.MapToSpecificationDto()

        };
    }

    public static List<ProductImageDto> MapToProductImageDto(List<ProductImage> productImages)
    {
        var images = new List<ProductImageDto>();
        foreach (var item in productImages)
        {
            images.Add(new ProductImageDto()
            {
                CreationDate = item.CreationDate,
                Id = item.Id,
                ImageName = item.ImageName,
                ProductId = item.ProductId,
                Sequence = item.Sequence,
            });
        }
        return images;
    }

    public static void SetCategoryProduct(this ProductDto product,ShopContext context)
    {
        var categories = context.Categories;
        var category = categories.FirstOrDefault(f => f.Id == product.CategoryId);
        

        var subCategory = categories.FirstOrDefault(f => f.Id == product.SubCategoryId);
        
        if (product.SecondarySubCategoryId != 0)
        {
            var  secondarySubCategory = categories.First(f => f.Id == product.SecondarySubCategoryId);
            product.SecondarySubCategory = secondarySubCategory.MapToProductCategoryDto();

        }


        product.Category = category.MapToProductCategoryDto();
        product.SubCategory = subCategory.MapToProductCategoryDto();
        
    }

    public static ProductCategoryDto MapToProductCategoryDto(this Category category)
    {
        return new ProductCategoryDto()
        {
            Id = category.Id,
            Title = category.Title,
            ParentId = category.ParentId,
            SeoData = category.SeoData,
            Slug = category.Slug,
        };
    }

    public static List<ProductSpecificationDto> MapToSpecificationDto(this List<ProductSpecification> specefications)
    {
        var result = new List<ProductSpecificationDto>();
        foreach (var item in specefications)
        {
            result.Add(new ProductSpecificationDto()
            {
                Key = item.Key,
                Value = item.Value,
            });
        }
        return result;
    }





}