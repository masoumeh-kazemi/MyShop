using Common.Application;
using MediatR;
using Shop.Application.Users.Register;
using Shop.Domain.UserAgg.Services;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByPhoneNumber;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> Register(UserRegisterCommand command);

    Task<UserDto> GetByPhoneNumber(string phoneNumber);
}

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Register(UserRegisterCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto> GetByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }
}