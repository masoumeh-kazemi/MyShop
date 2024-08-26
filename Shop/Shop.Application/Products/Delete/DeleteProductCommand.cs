using System.Drawing.Drawing2D;
using Common.Application;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Products.Delete;

public record DeleteProductCommand(long Id) : IBaseCommand;

public class DeleteProductCommandHandle : IBaseCommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandle(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.Id);
        if (product == null)
        {
            return OperationResult.NotFound();
        }

        _repository.Delete(product);
        await _repository.Save();
        return OperationResult.Success();
    }
}