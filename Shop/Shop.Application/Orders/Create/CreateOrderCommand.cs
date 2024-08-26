using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Orders.Create;

public class CreateOrderCommand : IBaseCommand
{
    public long UserId { get; private set; }
    public int Count { get; private set; }
    public long InventoryId { get; private set; }

    public CreateOrderCommand(long userId, int count, long inventoryId)
    {
        UserId = userId;
        Count = count;
        InventoryId = inventoryId;
    }
}

public class CreateOrderCommandHandler : IBaseCommandHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;
    public CreateOrderCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }
    public async Task<OperationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetCurrentOrder(request.UserId);
        if (order == null)
        {
            order = new Order(request.UserId);
            _orderRepository.Add(order);
        }

        var inventory = await _sellerRepository.GetInventoryById(request.InventoryId);
        if(inventory==null)
            return OperationResult.NotFound();

        if(request.Count> inventory.Count)
            return OperationResult.Error("تعداد درخواستی از مقدار موجود این فروشنده بیشتر است");

        
        order.AddOrderItem(request.Count,inventory.Price,inventory.DiscountPercentage , request.InventoryId);
        await _orderRepository.Save();
        return OperationResult.Success();


    }
}