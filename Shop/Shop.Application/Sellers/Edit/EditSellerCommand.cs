using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers.Edit;

public class EditSellerCommand : IBaseCommand
{
    public long SellerId { get; private set; }
    public string NationalCode { get; private set; }
    public string ShopName { get; private set; }
    public SellerStatus Status { get; private set; }

    public EditSellerCommand(long sellerId, string nationalCode, string shopName, SellerStatus status)
    {
        SellerId = sellerId;
        NationalCode = nationalCode;
        ShopName = shopName;
        Status = status;
    }
}

public class EditSellerCommandHandler : IBaseCommandHandler<EditSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _sellerDomainService;

    public EditSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService sellerDomainService)
    {
        _sellerRepository = sellerRepository;
        _sellerDomainService = sellerDomainService;
    }
    public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId);
        if (seller == null)
        {
            return OperationResult.NotFound();
        }

        seller.Edit(request.ShopName, request.NationalCode, request.Status, _sellerDomainService);
        await _sellerRepository.Save();
        return OperationResult.Success();
    }
}