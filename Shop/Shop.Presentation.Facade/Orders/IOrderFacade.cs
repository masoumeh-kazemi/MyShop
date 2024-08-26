using Common.Application;
using MediatR;
using Shop.Application.Orders.ChangeStatus;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.Create;
using Shop.Application.Orders.DecreaseItem;
using Shop.Application.Orders.Delete;
using Shop.Application.Orders.Finally;
using Shop.Application.Orders.IncreaseItem;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetCurrentByUserId;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult> Create(CreateOrderCommand command);
    Task<OperationResult> IncreaseItem(IncreaseOrderItemCommand command);
    Task<OperationResult> DecreaseItem(DecreaseOrderItemCommand command);
    Task<OperationResult> DeleteItem(DeleteOrderItemCommand command);
    Task<OperationResult> CheckoutOrder(CheckoutOrderCommand command);
    Task<OperationResult> FinallyOrder(FinallyOrderCommand command);
    Task<OperationResult> SendOrder(SendOrderCommand command);
    Task<OrderDto?> GetCurrent(long userId);
    Task<OrderDto> GetById(long id);

    Task<OrderFilterResult> GetByFilter(OrderFilterParam filterParam);

}

public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseItem(IncreaseOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItem(DecreaseOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteItem(DeleteOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> CheckoutOrder(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> FinallyOrder(FinallyOrderCommand command)
    {
        return  await _mediator.Send(command);
    }

    public async Task<OperationResult> SendOrder(SendOrderCommand command)
    {
        return await _mediator.Send(command);
    }


    public async Task<OrderDto?> GetCurrent(long userId)
    {
        return await _mediator.Send(new GetCurrentOrderByUserIdQuery(userId));
    }

    public async Task<OrderDto> GetById(long id)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id));
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParam filterParam)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParam));
    }
}