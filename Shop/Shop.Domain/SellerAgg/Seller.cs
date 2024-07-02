using System.Security.AccessControl;
using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    public long UserId { get; private set; }
    public string ShopName { get; private set; }
    public string NationalCode { get; private set; }
    public SellerStatus Status { get; private set; }
    public DateTime? LastUpdate { get; private set; }
    public List<SellerInventory> Inventories { get; private set; }

    private Seller()
    {

    }

    public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
    {
        //Guard(shopName, nationalCode);
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
        Inventories = new List<SellerInventory>();
        //if (domainService.IsValidSellerInformation(this) == false)
        //    throw new InvalidDomainDataException("اطلاعات امعتبر است");
    }

}