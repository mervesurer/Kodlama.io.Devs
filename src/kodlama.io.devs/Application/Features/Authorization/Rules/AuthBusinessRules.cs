using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            var result = await _userRepository.GetAsync(u => u.Email == email);
            if (result != null) 
                throw new BusinessException("Email address already exists");
        }

        public async Task CheckUserExists(string email)
        {
            var result = await _userRepository.GetAsync(u => u.Email == email);
            if (result == null) 
                throw new BusinessException("Email address could not be found");
        }

        public async Task CheckThePasswordIsCorrect(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException("Please make sure you entered password correctly");
        }
    }
}
