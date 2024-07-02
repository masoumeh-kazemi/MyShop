using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommand : IBaseCommand
{
    public string Title { get; private set; }
    public List<Permission> Permissions { get; private set; }

    public CreateRoleCommand(string title, List<Permission> permissions)
    {
        Title = title;
        Permissions = permissions;
    }
}

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var rolePermissions = new List<RolePermission>();
        foreach (var item in request.Permissions)
        {
          rolePermissions.Add(new RolePermission(item));  
        }
        var role = new Role(request.Title, rolePermissions);
        _roleRepository.Add(role);
        await _roleRepository.Save();
        return OperationResult.Success();
    }
}