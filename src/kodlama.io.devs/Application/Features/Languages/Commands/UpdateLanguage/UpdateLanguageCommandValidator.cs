using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Technologies.Commands.CreateTechnology;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    internal class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator() 
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
