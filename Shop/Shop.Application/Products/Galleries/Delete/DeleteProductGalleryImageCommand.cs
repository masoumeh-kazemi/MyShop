using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Products.Galleries.Delete;

public class DeleteProductGalleryImageCommand : IBaseCommand
{
    public DeleteProductGalleryImageCommand(long productId, long imageProductId)
    {
        ProductId = productId;
        ImageProductId = imageProductId;
    }

    public long ProductId { get; private set; }
    public long ImageProductId { get; private set; }
}

public class DeleteProductGalleryImageCommandHandler : IBaseCommandHandler<DeleteProductGalleryImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;
    public DeleteProductGalleryImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(DeleteProductGalleryImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if(product == null)
            return OperationResult.NotFound();

        product.DeleteImageGallery(request.ImageProductId);
        _fileService.DeleteFile(Directories.ProductGalleryImage, product.ImageName);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}