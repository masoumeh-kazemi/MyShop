using Common.Application;
using MediatR;
using Shop.Application.Products.Create;
using Shop.Application.Products.Delete;
using Shop.Application.Products.Edit;
using Shop.Application.Products.Galleries.Add;
using Shop.Application.Products.Galleries.Delete;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.Galleries.GetByList;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetByList;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
  Task<OperationResult> Create(CreateProductCommand command);
  Task<OperationResult> Edit(EditProductCommand command);
  Task<OperationResult> Delete(long id);


  Task<OperationResult> AddProductImage(AddProductGalleryImageCommand command);
  Task<OperationResult> DeleteProductImage(DeleteProductGalleryImageCommand command);


  Task<ProductDto> GetById(long id);
  Task<ProductDto> GetBySlug(string slug);
  Task<ProductFilterResult> GetByFilter(ProductFilterParam filterParam);
  Task<ProductShopResult> GetForShop(ProductShopFilterParam filterParam);
  Task<List<ProductDto>> GetList();

  //Task<List<ProductImageDto>> GetImageGalleries();

}

public class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;

    public ProductFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long id)
    {
        return await _mediator.Send(new DeleteProductCommand(id));
    }

    public async Task<OperationResult> AddProductImage(AddProductGalleryImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteProductImage(DeleteProductGalleryImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductDto> GetById(long id)
    {
        return await _mediator.Send(new GetProductByIdQuery(id));
    }

    public async Task<ProductDto> GetBySlug(string slug)
    {
        return await _mediator.Send(new GetProductBySlugQuery(slug));
    }

    public async Task<ProductFilterResult> GetByFilter(ProductFilterParam filterParam)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParam));
    }

    public async Task<ProductShopResult> GetForShop(ProductShopFilterParam filterParam)
    {
        return await _mediator.Send(new GetProductForShopQuery(filterParam));
    }

    public async Task<List<ProductDto>> GetList()
    {
        return await _mediator.Send(new GetProductByListQuery());
    }

    //public async Task<List<ProductImageDto>> GetImageGalleries()
    //{
    //    return await _mediator.Send(new GetProductImageByListQuery());
    //}
}