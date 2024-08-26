using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Users.Addresses.Create;
using Shop.Application.Users.Addresses.Delete;
using Shop.Application.Users.Addresses.Edit;
using Shop.Application.Users.Addresses.SetActiveAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users.Addresses;
using Shop.RazorPage.Pages.Infrastructure.RazorUtil;
using Shop.RazorPage.Pages.ViewModel;

namespace Shop.RazorPage.Pages.Profile.Addresses
{
    public class IndexModel : BaseRazorPage
    {
        public List<UserAddressDto> UserAddresses { get; set; }
        private readonly IUserAddressFacade _userAddressFacade;
        private readonly IRenderViewToString _renderViewToString;


        public IndexModel(IUserAddressFacade userAddressFacade, IRenderViewToString renderViewToString)
        {
            _userAddressFacade = userAddressFacade;
            _renderViewToString = renderViewToString;
        }
        public async Task OnGet()
        {
            var userAddresses = await _userAddressFacade.GetListAddress(User.GetUserId());
            UserAddresses = userAddresses;
        }

        public async Task<IActionResult> OnGetShowAddPage()
        {
            var view = await _renderViewToString.RenderToStringAsync("_Add", new AddAddressViewModel(), PageContext);
            return await AjaxTryCatch(async () =>
            {
                return OperationResult<string>.Success(view);
            });
        }


        public async Task<IActionResult> OnGetShowEditAddressPage(long addressId)
        {

            return await AjaxTryCatch(async () =>
            {
                var address = await _userAddressFacade.GetAddressById(addressId);
                var model = new EditAddressViewModel()
                {
                    AddressId = address.Id,
                    Name = address.Name,
                    Family = address.Family,
                    ActiveAddress = address.ActiveAddress,
                    City = address.City,
                    NationalCode = address.NationalCode,
                    PhoneNumber = address.PhoneNumber,
                    PostalAddress = address.PostalAddress,
                    PostalCode = address.PostalCode,
                    Shire = address.Shire
                };

                var view = await _renderViewToString.RenderToStringAsync("_Edit", model, PageContext);
                return OperationResult<string>.Success(view);
            });

        }


        public async Task<IActionResult> OnPostEditUserAddress(EditAddressViewModel editAddressView)
        {

            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressFacade.Edit(new EditUserAddressCommand(User.GetUserId(),
                    editAddressView.AddressId, editAddressView.Shire, editAddressView.City, editAddressView.PostalCode,
                    editAddressView.PostalAddress, editAddressView.PhoneNumber, editAddressView.Name, editAddressView.Family,
                    editAddressView.NationalCode));

                return result;
            });

        }
        public async Task<IActionResult> OnPostAddAddress(AddAddressViewModel addAddressView)
        {
            var model = new CreateUserAddressCommand(User.GetUserId(), addAddressView.Shire, addAddressView.City,
                addAddressView.PostalCode, addAddressView.PostalAddress, addAddressView.PhoneNumber, addAddressView.Name,
                addAddressView.Family, addAddressView.NationalCode);


            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressFacade.Create(model);
                return result;
            });
        }


        public async Task<IActionResult> OnPostDeleteUserAddress(long addressId)
        {
            var result = await _userAddressFacade.Delete(new DeleteUserAddressCommand(User.GetUserId(),
                addressId));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }


        public async Task<IActionResult> OnGetSetActiveAddress(long addressId)
        {

            return await AjaxTryCatch(async () =>
            {
                var result = await _userAddressFacade
                    .SetActiveAddress(new SetUserActiveAddressCommand(User.GetUserId(), addressId));
                return result;
            }, true);
        }
    }
}
