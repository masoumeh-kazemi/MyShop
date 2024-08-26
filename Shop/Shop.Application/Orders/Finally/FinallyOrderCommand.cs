using Common.Application;
using Shop.Domain.OrderAgg;

namespace Shop.Application.Orders.Finally;

public class FinallyOrderCommand : IBaseCommand
{
    public long Id { get; private set; }

    public FinallyOrderCommand(long id)
    {
        Id = id;
    }


    public class FinallyOrderCommandHandler : IBaseCommandHandler<FinallyOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public FinallyOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OperationResult> Handle(FinallyOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetTracking(request.Id);
            if (order == null)
            {
                return OperationResult.NotFound();
            }

            order.Finally();
            await _orderRepository.Save();
            return OperationResult.Success();
        }
    }
}