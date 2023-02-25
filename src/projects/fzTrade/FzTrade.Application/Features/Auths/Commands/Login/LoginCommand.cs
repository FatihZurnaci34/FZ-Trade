using Core.Security.Dtos;
using Core.Security.Entities;
using FzTrade.Application.Features.Auths.Dtos;
using FzTrade.Application.Features.Auths.Rules;
using FzTrade.Application.Services.AuthService;
using FzTrade.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Auths.Commands.Login
{
    public class LoginCommand:IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {
            private readonly IAuthService _authService;
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public LoginCommandHandler(IAuthService authService, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.CheckIfEmailIsCorrect(request.UserForLoginDto.Email);
                var user = await _userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);
                _authBusinessRules.CheckIfPasswordIsCorrect(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);
                var createdAccessToken = await _authService.CreateAccessToken(user);
                var createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                var addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                return new LoginedDto()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken
                };
            }
        }

    }
}
