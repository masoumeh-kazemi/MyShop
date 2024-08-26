using System.Formats.Asn1;
using Common.Application;
using MediatR;
using Shop.Application.Users.Addresses.Create;
using Shop.Application.Users.Addresses.Delete;
using Shop.Application.Users.Addresses.Edit;
using Shop.Application.Users.Addresses.SetActiveAddress;
using Shop.Query.Users.Addresses;
using Shop.Query.Users.Addresses.GetById;
using Shop.Query.Users.Addresses.GetByList;
using Shop.Query.Users.GetById;

namespace Shop.Presentation.Facade.Users.Addresses;

public interface IUserAddressFacade
{
    Task<OperationResult> Create(CreateUserAddressCommand command);
    Task<OperationResult> Edit(EditUserAddressCommand command);
    Task<OperationResult> Delete(DeleteUserAddressCommand command);
    Task<OperationResult> SetActiveAddress(SetUserActiveAddressCommand command);
    Task<UserAddressDto> GetAddressById(long id);
    Task<List<UserAddressDto>> GetListAddress(long userId);
}


public class UserAddressFacade : IUserAddressFacade
{
    private readonly IMediator _mediator;

    public UserAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetActiveAddress(SetUserActiveAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserAddressDto> GetAddressById(long id)
    {
        return await _mediator.Send(new GetUserAddressByIdQuery(id));
    }

    public async Task<List<UserAddressDto>> GetListAddress(long userId)
    {
        return await _mediator.Send(new GetUserAddressesListQuery(userId));
    }
}