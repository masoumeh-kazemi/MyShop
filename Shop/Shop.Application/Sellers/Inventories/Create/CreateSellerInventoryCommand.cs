using Common.Application;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.Inventories.Create;

public class CreateSellerInventoryCommand : IBaseCommand
{
    public long SellerId { get; private set; }
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }

    public CreateSellerInventoryCommand(long sellerId, long productId, int count, int price, int? discountPercentage)
    {
        SellerId = sellerId;
        ProductId = productId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }
}


public class CreateSellerInventoryCommandHandler : IBaseCommandHandler<CreateSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public CreateSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }
    public async Task<OperationResult> Handle(CreateSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller == null)
        {
            return OperationResult.NotFound();
        }

        var inventory = new SellerInventory(request.ProductId, request.Count, request.Price,
            request.DiscountPercentage);

        seller.AddInventory(inventory);
        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}