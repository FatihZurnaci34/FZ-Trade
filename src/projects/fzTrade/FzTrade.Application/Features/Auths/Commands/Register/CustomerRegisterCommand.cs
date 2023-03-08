using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using FzTrade.Application.Features.Auths.Dtos;
using FzTrade.Application.Features.Auths.Rules;
using FzTrade.Application.Services.AuthService;
using FzTrade.Application.Services.Repositories;
using FzTrade.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzTrade.Application.Features.Auths.Commands.Register
{
    public class CustomerRegisterCommand:IRequest<RegisteredDto>
    {
        public CustomerForRegister CustomerForRegister { get; set; }
        public string IpAddress { get; set; }

        public class CustomerRegisterCommandHandler : IRequestHandler<CustomerRegisterCommand, RegisteredDto>
        {
            private readonly IAuthService _authService;
            private readonly ICustomerRepository _customerRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public CustomerRegisterCommandHandler(IAuthService authService, ICustomerRepository customerRepository, AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _customerRepository = customerRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RegisteredDto> Handle(CustomerRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.CustomerForRegister.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.CustomerForRegister.Password, out passwordHash, out passwordSalt);
                Customer newCustomer = new()
                {
                    Email = request.CustomerForRegister.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.CustomerForRegister.FirstName,
                    LastName = request.CustomerForRegister.LastName,
                    Gender = request.CustomerForRegister.Gender,
                    Status = true
                };
                Customer createdUser = await _customerRepository.AddAsync(newCustomer);
                await _authService.CreateAndAddUserClaim(createdUser);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };
                return registeredDto;
            }
        }
    }
}
