using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommand : IBaseCommand
{
    public CreateSellerCommand(long userId, string nationalCode, string shopName)
    {
        UserId = userId;
        NationalCode = nationalCode;
        ShopName = shopName;
    }

    public long UserId { get; private set; }
    public string NationalCode { get; private set; }
    public string ShopName { get; private set; }
}

public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _sellerDomainService;


    public CreateSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService sellerDomainService)
    {
        _sellerRepository = sellerRepository;
        _sellerDomainService = sellerDomainService;
    }
    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserId, request.ShopName
            , request.NationalCode, _sellerDomainService);

        _sellerRepository.Add(seller);
        await _sellerRepository.Save();
        return OperationResult.Success("نتیجه ثبت نام با پیامک اطلاع داده می شود");
    }
}