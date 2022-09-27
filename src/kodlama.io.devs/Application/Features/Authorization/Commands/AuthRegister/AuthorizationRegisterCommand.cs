using Application.Features.Authorization.Dtos;
using Application.Features.Authorization.Rules;
using Application.Services;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.AuthRegister
{
    public class AuthorizationRegisterCommand : IRequest<AuthRegisterDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }

        public class AuthorizationRegisterCommandHandler : IRequestHandler<AuthorizationRegisterCommand, AuthRegisterDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public AuthorizationRegisterCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IOperationClaimRepository operationClaimRepository, IUserOperationClaimRepository userOperationClaimRepository, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _operationClaimRepository = operationClaimRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<AuthRegisterDto> Handle(AuthorizationRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Email = request.UserForRegisterDto.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Status = true,
                    AuthenticatorType = AuthenticatorType.Email
                };
                User newUser = await _userRepository.AddAsync(user);

                OperationClaim? claim = await _operationClaimRepository.GetAsync(p => p.Name == "User");
                UserOperationClaim userOperationClaim = new UserOperationClaim{ UserId = newUser.Id, OperationClaimId = claim.Id};
                await _userOperationClaimRepository.AddAsync(userOperationClaim);

                var userClaims = await _userOperationClaimRepository.GetListAsync(
                    p => p.UserId == newUser.Id,
                    include: p => p.Include(c => c.OperationClaim),
                    cancellationToken: cancellationToken);

                var authRegisterDto = new AuthRegisterDto();

                authRegisterDto.AccessToken = _tokenHelper.CreateToken(newUser, userClaims.Items.Select(p => p.OperationClaim).ToList());
                return authRegisterDto;
            }
        }
    }
}
