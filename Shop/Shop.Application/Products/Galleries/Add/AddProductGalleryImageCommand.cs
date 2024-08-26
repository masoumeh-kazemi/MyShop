using Common.Application;
using Common.Application._Utilities;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application.Products.Galleries.Add;
using Shop.Domain.ProductAgg;

namespace Shop.Application.Products.Galleries.Add;

public class AddProductGalleryImageCommand : IBaseCommand
{
    public long ProductId { get; private set; }
    public IFormFile ImageFile { get; private set; }
    public int Sequence { get; private set; }

    public AddProductGalleryImageCommand(long productId, IFormFile imageFile, int sequence)
    {
        ProductId = productId;
        ImageFile = imageFile;
        Sequence = sequence;
    }
}

public class AddProductGalleryImageCommandHandler : IBaseCommandHandler<AddProductGalleryImageCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IFileService _fileService;

    public AddProductGalleryImageCommandHandler(IProductRepository productRepository, IFileService fileService)
    {
        _productRepository = productRepository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(AddProductGalleryImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetTracking(request.ProductId);
        if (product == null)
        {
            return OperationResult.NotFound();
        }

        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);
        var productImage = new ProductImage(imageName, request.Sequence);

        product.AddImageGallery(productImage);
        await _productRepository.Save();
        return OperationResult.Success();
    }
}