using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.Banners.GetByList;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Banners;

public interface IBannerFacade
{
    Task<OperationResult> Create(CreateBannerCommand command);
    Task<OperationResult> Edit(EditBannerCommand command);

    Task<BannerDto> GetById(long id);
    Task<List<BannerDto>> GetList();
}

public class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;

    public BannerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<BannerDto> GetById(long id)
    {
        return await _mediator.Send(new GetBannerByIdQuery(id));
    }

    public async Task<List<BannerDto>> GetList()
    {
        return await _mediator.Send(new GetBannerByListQuery());
    }
}