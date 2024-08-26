using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Sliders.GetById;
using Shop.Query.SiteEntities.Sliders.GetByList;

namespace Shop.Presentation.Facade.SiteEntities.Sliders;

public interface ISliderFacade
{
    Task<OperationResult> Create(CreateSliderCommand command);
    Task<OperationResult> Edit(EditSliderCommand command);

    Task<SliderDto> GetById(long id);
    Task<List<SliderDto>> GetByList();
}

public class SliderFacade : ISliderFacade
{

    private readonly IMediator _mediator;

    public SliderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<SliderDto> GetById(long id)
    {
        return await _mediator.Send(new GetSliderByIdQuery(id));
    }

    public async Task<List<SliderDto>> GetByList()
    {
        return await _mediator.Send(new GetSliderByListQuery());
    }
}