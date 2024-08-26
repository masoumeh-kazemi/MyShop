using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.ChangeStatus;

public class SendOrderCommand : IBaseCommand
{
    public long OrderId { get; private set; }
    public OrderStatus Status { get; private set; }

    public SendOrderCommand(long orderId, OrderStatus status)
    {
        OrderId = orderId;
        Status = status;
    }
}

public class SendOrderCommandHandler : IBaseCommandHandler<SendOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public SendOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public  async Task<OperationResult> Handle(SendOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetTracking(request.OrderId);
        if (order == null)
        {
            return OperationResult.NotFound();
        }
         
        order.ChangeStatus(request.Status);
        await _orderRepository.Save();
        return OperationResult.Success();
    }
}