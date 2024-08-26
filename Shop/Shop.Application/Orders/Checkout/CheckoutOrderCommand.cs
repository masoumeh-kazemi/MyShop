using Common.Application;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.ValueObjects;
using Shop.Domain.SiteEntities;
using Shop.Domain.UserAgg;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommand : IBaseCommand
{
    public CheckoutOrderCommand(long userId, string shire, string city, string postalCode, string postalAddress, string phoneNumber, string name, string family, string nationalCode, long shippingMethodId)
    {
        UserId = userId;
        Shire = shire;
        City = city;
        PostalCode = postalCode;
        PostalAddress = postalAddress;
        PhoneNumber = phoneNumber;
        Name = name;
        Family = family;
        NationalCode = nationalCode;
        ShippingMethodId = shippingMethodId;
    }
    public long UserId { get; private set; }
    public string Shire { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public string PostalAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string NationalCode { get; private set; }
    public long ShippingMethodId { get; private set; }
}

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IShippingMethodRepository _shippingMethodRepository;
    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IUserRepository userRepository, IShippingMethodRepository shippingMethodRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _shippingMethodRepository = shippingMethodRepository;
    }
    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _orderRepository.GetCurrentOrder(request.UserId);
        if (currentOrder == null)
        {
            return OperationResult.NotFound();
        }

        var address = new OrderAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
            request.PhoneNumber, request.Name, request.Family, request.NationalCode);
        currentOrder.SetAddress(address);

        var shipping = await _shippingMethodRepository.GetTracking(request.ShippingMethodId);
        var shippingMethod = new OrderShippingMethod(shipping.Title, shipping.Cost);
        currentOrder.SetShippingMethod(shippingMethod);

        await _orderRepository.Save();
        return OperationResult.Success();
    }
}