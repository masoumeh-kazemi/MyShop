using Common.Application;
using MediatR;
using Shop.Application.Sellers.Inventories.Create;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Query.Sellers.Inventories.DTOs;
using Shop.Query.Sellers.Inventories.GetById;
using Shop.Query.Sellers.Inventories.GetByList;
using Shop.Query.Sellers.Inventories.GetByProductId;

namespace Shop.Presentation.Facade.Sellers.Inventories;

public interface ISellerInventoryFacade
{
   Task<OperationResult> Create(CreateSellerInventoryCommand command);
   Task<OperationResult> Edit(EditSellerInventoryCommand command);

   Task<InventoryDto?> GetById(long id);
   Task<List<InventoryDto>> GetByList(long sellerId);
   Task<List<InventoryDto>?> GetListByProductId(long productId);
}

public class SellerInventoryFacade : ISellerInventoryFacade
{
    private readonly IMediator _mediator;

    public SellerInventoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<InventoryDto?> GetById(long id)
    {
        return await _mediator.Send(new GetSellerInventoryById(id));
    }

    public async Task<List<InventoryDto>> GetByList(long sellerId)
    {
        return await _mediator.Send(new GetSellerInventoryByListQuery(sellerId));
    }

    public async Task<List<InventoryDto>?> GetListByProductId(long productId)
    {
        return await _mediator.Send(new GetInventoryListByProductIdQuery(productId));
    }
}