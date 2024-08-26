using Common.Application;
using MediatR;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByList;
using Shop.Query.Categories.GetByParentId;

namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> AddChild(AddCategoryChildCommand command);


    Task<CategoryDto> GetById(long id);
    Task<List<CategoryDto>> GetByList();
    Task<List<ChildCategoryDto>?> GetChildByParentId(long parentId);
}

public class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;

    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddChild(AddCategoryChildCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CategoryDto> GetById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<CategoryDto>> GetByList()
    {
        return await _mediator.Send(new GetCategoryByListQuery());
    }

    public async Task<List<ChildCategoryDto>?> GetChildByParentId(long parentId)
    {
        return await _mediator.Send(new GetChildCategoryByParentIdQuery(parentId));
    }
}