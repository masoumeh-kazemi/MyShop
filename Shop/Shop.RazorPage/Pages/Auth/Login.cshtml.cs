using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Shop.Application.Orders.Create;
using Shop.Application.Users.AddToken;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using Shop.RazorPage.Pages.Admin.DTOs;
using Shop.RazorPage.Pages.Infrastructure.CookieUtils;
using Shop.RazorPage.Pages.Infrastructure.JwtUtil;
using UAParser;

namespace Shop.RazorPage.Pages.Auth
{
    [BindProperties]
    public class LoginModel : PageModel
    {   
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]

        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private readonly IUserFacade _userFacade;
        private readonly IConfiguration _configuration;
        private readonly ShopCartCookieManager _cartCookieManager;
        private readonly IOrderFacade _orderFacade;

        public LoginModel(IUserFacade userFacade, IConfiguration configuration, ShopCartCookieManager cartCookieManager, IOrderFacade orderFacade)
        {
            _userFacade = userFacade;
            _configuration = configuration;
            _cartCookieManager = cartCookieManager;
            _orderFacade = orderFacade;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetByPhoneNumber(PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError(nameof(PhoneNumber), "کاربری با مشخصات داده شده یافت نشد");
                return Page();
            }
            if (user.IsActive == false)
            {
                ModelState.AddModelError(nameof(PhoneNumber), "کاربری با مشخصات داده شده یافت نشد");
                return Page();
            }

            var isPasswordCorrect = Sha256Hasher.IsCompare(user.Password, Password);
            if (isPasswordCorrect == false)
            {
                ModelState.AddModelError(nameof(PhoneNumber), "کاربری با مشخصات داده شده یافت نشد");
                return Page();
            }
            var loginResult = AddTokenAndGenerateJwt(user);
            var token = loginResult.Result.Data.Token;
            var refreshToken = loginResult.Result.Data.RefreshToken;

            HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7)
            });

            HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(10)
            });

            //HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
            return Redirect("/");
        }

        private async Task<OperationResult<LoginResultDto>> AddTokenAndGenerateJwt(UserDto user)
        {
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers["user-agent"].ToString();
            var device = "windows";

            if (string.IsNullOrWhiteSpace(header) == false)
            {
                var info = uaParser.Parse(header);
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            }

            var jwtToken = JwtTokenBuilder.BuildToken(user, _configuration);
            var refreshToken = Guid.NewGuid().ToString();
            var hashJwtToken = Sha256Hasher.Hash(jwtToken);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
            await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwtToken, hashRefreshToken,
                DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));

            var loginResult = new LoginResultDto()
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            };
            await SyncShopCart(jwtToken);

            return OperationResult<LoginResultDto>.Success(loginResult);
        }


        private async Task SyncShopCart(string token)
        {
            var shopCart =  _cartCookieManager.GetShopCart();
            if (shopCart == null|| shopCart.OrderItems.Any()==false)
            {
                return;
            }

            HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            foreach (var item in shopCart.OrderItems)
            {
                await _orderFacade.Create(new CreateOrderCommand(userId, item.Count, item.InventoryId));
            }

            _cartCookieManager.DeleteShopCart();

        }
    }
}
