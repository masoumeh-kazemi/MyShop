using Common.Application;
using MediatR;
using Shop.Application.Products.Create;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;
using Shop.Query.Sellers.GetByUserId;
using Shop.Query.Sellers.Inventories.DTOs;
using Shop.Query.Sellers.Inventories.GetByList;

namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{
    Task<OperationResult> Create(CreateSellerCommand command);
    Task<OperationResult> Edit(EditSellerCommand command);

    Task<SellerDto> GetById(long id);
    Task<SellerDto> GetByUserId(long userId);
    Task<SellerFilterResult> GetByFilter(SellerFilterParam filterParam);

}

public class SellerFacade : ISellerFacade
{
    private readonly IMediator _mediator;

    public SellerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<SellerDto> GetById(long id)
    {
        return await _mediator.Send(new GetSellerByIdQuery(id));
    }

    public async Task<SellerDto> GetByUserId(long userId)
    {
        return await _mediator.Send(new GetSellerByUserIdQuery(userId));
    }

    public async Task<SellerFilterResult> GetByFilter(SellerFilterParam filterParam)
    {
        return await _mediator.Send(new GetSellerByFilterQuery(filterParam));
    }
}