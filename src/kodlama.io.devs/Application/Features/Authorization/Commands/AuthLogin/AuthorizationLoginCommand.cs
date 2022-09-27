using Application.Features.Authorization.Dtos;
using Application.Features.Authorization.Rules;
using Application.Services;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.AuthLogin
{
    public class AuthorizationLoginCommand : IRequest<AuthLoginDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class AuthorizationLoginCommandHandler : IRequestHandler<AuthorizationLoginCommand, AuthLoginDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private readonly AuthBusinessRules _authBusinessRules;

            public AuthorizationLoginCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IRefreshTokenRepository refreshTokenRepository, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _refreshTokenRepository = refreshTokenRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<AuthLoginDto> Handle(AuthorizationLoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
                await _authBusinessRules.CheckUserExists(user.Email);
                await _authBusinessRules.CheckThePasswordIsCorrect(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                var userClaims = await _userOperationClaimRepository.GetListAsync(uc => uc.UserId == user.Id,
                    include: u => u.Include(c => c.OperationClaim));

                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));

                AccessToken accessToken = _tokenHelper.CreateToken(user, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());
                RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString());
                RefreshToken newrefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);


                AuthLoginDto authLoginDto = new(){ AccessToken = accessToken };
                return authLoginDto;
            }
        }
    }
}
