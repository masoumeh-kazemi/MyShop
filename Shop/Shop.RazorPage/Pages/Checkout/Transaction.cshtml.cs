using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.Finally;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.SiteEntities.ShippingMethods;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Orders.DTOs;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.Users.Addresses;
using Shop.RazorPage.Pages.Infrastructure.Gateways.Zibal.DTOs;
using Shop.RazorPage.Pages.Infrastructure.Gateways.Zibal;

namespace Shop.RazorPage.Pages.Checkout
{
    public class TransactionModel : PageModel
    {
        public List<UserAddressDto> Addresses { get; set; }
        public OrderDto CurrentOrder { get; set; }

        public List<ShippingMethodDto> ShippingMethods { get; set; }


        private readonly IUserAddressFacade _userAddressFacade;
        private readonly IOrderFacade _orderFacade;
        private readonly IShippingMethodFacade _shippingMethodFacade;
        private readonly IZibalService _zibalService;


        public TransactionModel(IUserAddressFacade userAddressFacade, IOrderFacade orderFacade, IShippingMethodFacade shippingMethodFacade, IZibalService zibalService)
        {
            _userAddressFacade = userAddressFacade;
            _orderFacade = orderFacade;
            _shippingMethodFacade = shippingMethodFacade;
            _zibalService = zibalService;
        }

        public async Task<IActionResult> OnPost(long shippingMethodId, long orderId, string errorRedirect = "", string successRedirect = "")
        {
            var addresses = await _userAddressFacade.GetListAddress(User.GetUserId());
            var activeAddress = addresses.FirstOrDefault(f => f.ActiveAddress);
            if (activeAddress == null)
            {
                return Redirect("Index");
            }

            var res = await _orderFacade.CheckoutOrder(new CheckoutOrderCommand(User.GetUserId(),
                activeAddress.Shire, activeAddress.City, activeAddress.PostalCode, activeAddress.PostalAddress,
                activeAddress.PhoneNumber, activeAddress.Name, activeAddress.Family, activeAddress.NationalCode, shippingMethodId));

            //if (res.Status == OperationResultStatus.Success)
            //{
            var userId = User.GetUserId();
            var order = await _orderFacade.GetCurrent(User.GetUserId());
            
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var errorCallBackUrl2 =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{orderId}";
            var successCallBackUrl2 =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{orderId}";

            var result = await _zibalService.StartPay(new ZibalPaymentRequest()
            {
                Amount = order.TotalPrice,
                CallBackUrl = $"{url}/checkout/transaction?orderId={order.Id}&&errorRedirect={errorCallBackUrl2}&&successRedirect={successCallBackUrl2}&&shippingMethodId={shippingMethodId}",
                Description = $"پرداخت سفارش با شناسه {order.Id}",
                LinkToPay = false,
                Merchant = "zibal",
                SendSms = false,
                Mobile = User.GetPhoneNumber(),
            });
            return Redirect(result);

            //}

            ////return Page();
        }
        public async Task<IActionResult> OnGet(long orderId, long trackId, int success, string errorRedirect, string successRedirect)
        {
            if (success == 0)
                return Redirect(errorRedirect);

            var order = await _orderFacade.GetById(orderId);

            if (order == null || order.Address == null || order.ShippingMethod == null)
                return Redirect(errorRedirect);

            if (order == null)
                return Redirect(errorRedirect);

            var result = await _zibalService.Verify(new ZibalVeriyfyRequest(trackId, "zibal"));
            //if (result.Status != 100)
            //    return Redirect(errorRedirect);

            if (result.Amount != order.TotalPrice)
                return Redirect(errorRedirect);

            var commandResult = await _orderFacade.FinallyOrder(new FinallyOrderCommand(order.Id));
            if (commandResult.Status == OperationResultStatus.Success)
                return Redirect(successRedirect);

            return Redirect(errorRedirect);
        }


    }
}
