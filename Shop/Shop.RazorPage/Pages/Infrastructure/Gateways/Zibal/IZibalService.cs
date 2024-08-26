using Shop.RazorPage.Pages.Infrastructure.Gateways.Zibal.DTOs;

namespace Shop.RazorPage.Pages.Infrastructure.Gateways.Zibal;

public interface IZibalService
{
    Task<string> StartPay(ZibalPaymentRequest request);
    Task<ZibalVeriyfyResponse> Verify(ZibalVeriyfyRequest request);
}