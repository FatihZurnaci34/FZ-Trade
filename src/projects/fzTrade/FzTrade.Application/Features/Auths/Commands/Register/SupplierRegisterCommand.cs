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
    public class SupplierRegisterCommand:IRequest<RegisteredDto>
    {
        public SupplierForRegister SupplierForRegister { get; set; }
        public string IpAddress { get; set; }

        public class SupplierRegisterCommandHandler : IRequestHandler<SupplierRegisterCommand, RegisteredDto>
        {
            private readonly IAuthService _authService;
            private readonly ISupplierRepository _supplierRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public SupplierRegisterCommandHandler(IAuthService authService, ISupplierRepository supplierRepository, AuthBusinessRules authBusinessRules)
            {
                _authService = authService;
                _supplierRepository = supplierRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<RegisteredDto> Handle(SupplierRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailCanNotBeDuplicatedWhenRegistered(request.SupplierForRegister.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.SupplierForRegister.Password, out passwordHash, out passwordSalt);
                Supplier newSupplier = new()
                {
                    Email = request.SupplierForRegister.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.SupplierForRegister.FirstName,
                    LastName = request.SupplierForRegister.LastName,
                    CompanyName = request.SupplierForRegister.CompanyName,
                    Location = request.SupplierForRegister.Location,
                    Status = true
                };
                Supplier createdSupplier = await _supplierRepository.AddAsync(newSupplier);
                await _authService.CreateAndAddUserClaim(createdSupplier);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdSupplier);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdSupplier, request.IpAddress);
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
