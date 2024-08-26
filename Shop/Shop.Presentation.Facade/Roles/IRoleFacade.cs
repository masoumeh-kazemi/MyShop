using Common.Application;
using MediatR;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles.DTOs;
using Shop.Query.Roles.GetById;
using Shop.Query.Roles.GetList;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> Create(CreateRoleCommand command);
    Task<OperationResult> Edit(EditRoleCommand command);

    Task<RoleDto> GetById(long id);
    Task<List<RoleDto>> GetList();

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

    public async Task<OperationResult> Edit(EditRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<RoleDto> GetById(long id)
    {
        return await _mediator.Send(new GetRoleByIdQuery(id));
    }

    public async Task<List<RoleDto>> GetList()
    {
        return await _mediator.Send(new GetRoleByListQuery());
    }
}