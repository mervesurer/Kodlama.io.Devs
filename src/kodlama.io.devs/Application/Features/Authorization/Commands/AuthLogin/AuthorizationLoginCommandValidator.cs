using Application.Features.Authorization.Commands.AuthRegister;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Commands.AuthLogin
{
    public class AuthorizationLoginCommandValidator : AbstractValidator<AuthorizationLoginCommand>
    {
        public AuthorizationLoginCommandValidator()
        {
            RuleFor(u => u.UserForLoginDto.Email).NotEmpty();
            RuleFor(u => u.UserForLoginDto.Password).NotEmpty();
        }        
    }
}
