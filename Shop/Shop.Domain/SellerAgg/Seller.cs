﻿using System.Security.AccessControl;
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

    public void Edit(string shopName, string nationalCode, SellerStatus status, ISellerDomainService domainService)
    {
        ShopName = shopName;
        NationalCode = nationalCode;
        Status = status;

    }

    public void AddInventory(SellerInventory inventory)
    {
        inventory.SellerId = Id;
        Inventories.Add(inventory);
    }

    public void EditInventory(long inventoryId, int count, int price, int? discountPercentage)
    {
        var inventory = Inventories.FirstOrDefault(f => f.Id == inventoryId);
        if (inventory == null)
            throw new NullOrEmptyDomainDataException();

        inventory.Edit(count, price, discountPercentage);
    }
}