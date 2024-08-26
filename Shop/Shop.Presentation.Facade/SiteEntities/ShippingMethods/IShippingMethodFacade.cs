using MediatR;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.ShippingMethods.GetList;

namespace Shop.Presentation.Facade.SiteEntities.ShippingMethods;

public interface IShippingMethodFacade
{
    Task<List<ShippingMethodDto>> GetList();
}

public class ShippingMethodFacade : IShippingMethodFacade
{
    private readonly IMediator _mediator;

    public ShippingMethodFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<List<ShippingMethodDto>> GetList()
    {
        return await _mediator.Send(new GetShippingMethodQuery());
    }
}