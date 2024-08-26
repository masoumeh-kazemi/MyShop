using Common.Application;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Sellers.Inventories.Edit;

public class EditSellerInventoryCommand : IBaseCommand
{
    public long SellerId { get; private set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int? DiscountPercentage { get; private set; }

    public EditSellerInventoryCommand(long sellerId, long inventoryId, int count, int price, int? discountPercentage)
    {
        SellerId = sellerId;
        InventoryId = inventoryId;
        Count = count;
        Price = price;
        DiscountPercentage = discountPercentage;
    }
}

public class EditSellerInventoryCommandHandler : IBaseCommandHandler<EditSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public EditSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }
    public async Task<OperationResult> Handle(EditSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if(seller == null)
            return OperationResult.NotFound();
         
        seller.EditInventory(request.InventoryId, request.Count, request.Price, request.DiscountPercentage);
        await _sellerRepository.Save();
        return OperationResult.Success();

    }
}