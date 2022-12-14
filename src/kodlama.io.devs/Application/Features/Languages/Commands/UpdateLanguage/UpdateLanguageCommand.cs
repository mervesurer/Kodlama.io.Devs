using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<UpdatedLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdatedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<UpdatedLanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {                
                await _languageBusinessRules.LanguageCanNotBeDublicateted(request.Name);

                Language? language = await _languageRepository.GetAsync(lang => lang.Id == request.Id);
                await _languageBusinessRules.LanguageShouldExistWhenRequested(language);
                language.Name = request.Name;
                Language updatedLanguage = await _languageRepository.UpdateAsync(language);
                    
                UpdatedLanguageDto updatedLanguageDto = _mapper.Map<UpdatedLanguageDto>(updatedLanguage);
                return updatedLanguageDto;
            }
        }
    }
}
