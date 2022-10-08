using Application.Features.Languages.Commands.UpdateLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.UpdateOpreationClaim
{
    public class UpdateOperationClaimCommanValidator : AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimCommanValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
