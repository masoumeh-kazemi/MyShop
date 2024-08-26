using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.IncreaseItem;

public class IncreaseOrderItemCommand : IBaseCommand
{
    public int Count { get; private set; }
    public long ItemId { get; private set; }
    public long UserId { get; private set; }

    public IncreaseOrderItemCommand(int count, long itemId, long userId)
    {
        Count = count;
        ItemId = itemId;
        UserId = userId;
    }
}

public class IncreaseOrderItemCommandHandler : IBaseCommandHandler<IncreaseOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public IncreaseOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<OperationResult> Handle(IncreaseOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrder(request.UserId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }
        order.IncreaseItemCount(request.ItemId, request.Count);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}