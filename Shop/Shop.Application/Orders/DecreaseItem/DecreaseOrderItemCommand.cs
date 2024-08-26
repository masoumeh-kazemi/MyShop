using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.DecreaseItem;

public class DecreaseOrderItemCommand : IBaseCommand
{
    public DecreaseOrderItemCommand(int count, long itemId, long userId)
    {
        Count = count;
        ItemId = itemId;
        UserId = userId;
    }
    public int Count { get; private set; }
    public long ItemId { get; private set; }
    public long UserId { get; private set; }
}

public class DecreaseOrderItemCommandHandler : IBaseCommandHandler<DecreaseOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DecreaseOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<OperationResult> Handle(DecreaseOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrder(request.UserId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }

        order.DecreaseItemCount(request.ItemId, request.Count);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}