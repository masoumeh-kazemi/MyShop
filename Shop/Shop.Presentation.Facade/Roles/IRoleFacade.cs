using Common.Application;
using MediatR;
using Shop.Application.Roles.Create;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> Create(CreateRoleCommand command);
}

public class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;

    public RoleFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }
}