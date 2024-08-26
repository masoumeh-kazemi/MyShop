using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Application.Roles.Edit;

public class EditRoleCommand : IBaseCommand
{
    public long Id { get; private set; }
    public string Title { get; private set; }
    public List<Permission> Permissions{ get; private set; }

    public EditRoleCommand(long id, string title, List<Permission> permissions)
    {
        Id = id;
        Title = title;
        Permissions = permissions;
    }
}


public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public EditRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetTracking(request.Id);
        role.Edit(request.Title);

        await _roleRepository.Save();
        return OperationResult.Success();
    }
}