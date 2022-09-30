using Application.Features.Authorization.Commands.AuthLogin;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
    {
        public CreateGitHubProfileCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.GitHubUrl).NotEmpty();
        }
    }
}
