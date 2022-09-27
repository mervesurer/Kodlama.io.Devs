using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.AuthRegister
{
    public class AuthorizationRegisterCommandValidator : AbstractValidator<AuthorizationRegisterCommand>
    {
        public AuthorizationRegisterCommandValidator()
        {
            RuleFor(u => u.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Email).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Password).NotEmpty();
            RuleFor(u => u.UserForRegisterDto.Email).EmailAddress();
            RuleFor(u => u.UserForRegisterDto.Password).MinimumLength(8);
        }
    }
}
