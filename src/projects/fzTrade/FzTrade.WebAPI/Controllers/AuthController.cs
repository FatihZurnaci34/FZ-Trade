using Core.Security.Dtos;
using Core.Security.Entities;
using FzTrade.Application.Features.Auths.Commands.Login;
using FzTrade.Application.Features.Auths.Commands.Register;
using FzTrade.Application.Features.Auths.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FzTrade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("RegisterForCustomer")]
        public async Task<IActionResult> RegisterForCustomer([FromBody] CustomerForRegister customerForRegister)
        {
            CustomerRegisterCommand customerRegisterCommand = new()
            {
                CustomerForRegister = customerForRegister,
                IpAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(customerRegisterCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("RegisterForSupplier")]
        public async Task<IActionResult> RegisterForSupplier([FromBody] SupplierForRegister supplierForRegister)
        {
            SupplierRegisterCommand supplierRegisterCommand = new()
            {
                SupplierForRegister = supplierForRegister,
                IpAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(supplierRegisterCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var loginCommand = new LoginCommand
            {
                UserForLoginDto = userForLoginDto,
                IpAddress = GetIpAddress()
            };

            var result = await Mediator!.Send(loginCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
