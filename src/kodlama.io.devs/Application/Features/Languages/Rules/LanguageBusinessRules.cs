using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Languages.Rules
{
    public class LanguageBusinessRules
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task LanguageCanNotBeDublicateted(string name)
        {
            IPaginate<Language> result = await _languageRepository.GetListAsync(lang => lang.Name == name);
            if (result.Items.Any())
                throw new BusinessException("Language name exists.");
        }

        public async Task LanguageShouldExistWhenRequested(Language? language)
        {
            if (language == null)
                throw new BusinessException("This Id does not exists.");
        }
    }
}
