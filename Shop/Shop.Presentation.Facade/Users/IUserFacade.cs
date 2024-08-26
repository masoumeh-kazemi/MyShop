using Common.Application;
using MediatR;
using Shop.Application.Roles.Edit;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.DeleteToken;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditProfile;
using Shop.Application.Users.Register;
using Shop.Domain.UserAgg.Services;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.GetUsersByFilter;
using Shop.Query.Users.GetUserTokenByToken;

namespace Shop.Presentation.Facade.Users;

public interface IUserFacade
{
    Task<OperationResult> Register(UserRegisterCommand command);
    Task<OperationResult> Create(CreateUserCommand command);
    Task<OperationResult> Edit(EditUserCommand command);
    Task<OperationResult> EditProfile(EditUserProfileCommand command);

    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);


    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> DeleteToken(DeleteUserTokenCommand command);

    Task<UserDto> GetByPhoneNumber(string phoneNumber);
    Task<UserDto> GetById(long id);
    Task<UserTokenDto> GetUserTokenByJwtToken(string jwtToken);
    Task<UserFilterResult> GetByFilter(UserFilterParam filterParam);
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

    public async Task<OperationResult> Create(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProfile(EditUserProfileCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteToken(DeleteUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto> GetByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserDto> GetById(long id)
    {
        return await _mediator.Send(new GetUserByIdQuery(id));
    }

    public async Task<UserTokenDto> GetUserTokenByJwtToken(string jwtToken)
    {
        return await _mediator.Send(new GetUserTokenByTokenQuery(jwtToken));
    }

    public async Task<UserFilterResult> GetByFilter(UserFilterParam filterParam)
    {
        return await _mediator.Send(new GetUsersByFilterQuery(filterParam));
    }
}