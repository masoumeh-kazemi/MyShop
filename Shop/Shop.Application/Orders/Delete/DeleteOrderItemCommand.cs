using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.Delete;

public class DeleteOrderItemCommand : IBaseCommand
{
    public long ItemId { get; private set; }
    public long UserId { get; private set; }

    public DeleteOrderItemCommand(long itemId, long userId)
    {
        ItemId = itemId;
        UserId = userId;
    }


}
public class DeleteOrderItemCommandHandler : IBaseCommandHandler<DeleteOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<OperationResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrder(request.UserId);
        if (order == null)
        {
            return OperationResult.Error("این سفارش وجود ندارد");
        }

        order.DeleteOrderItem(request.ItemId);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}


