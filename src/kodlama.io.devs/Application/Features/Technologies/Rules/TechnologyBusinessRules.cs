using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ILanguageRepository _languageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, ILanguageRepository languageRepository)
        {
            _technologyRepository = technologyRepository;
            _languageRepository = languageRepository;
        }

        public async Task TechnologyCanNotBeDublicateted(string name, int languageId)
        {
            Technology? result = await _technologyRepository.GetAsync(lang => lang.Name == name
                                                                               && lang.LanguageId == languageId);
            if (result != null)
                throw new BusinessException("Technology exists.");
        }

        public async Task TechnologyShouldExistWhenRequested(Technology? technology)
        {
            if (technology == null)
                throw new BusinessException("This Id does not exists.");
        }

        public async Task LanguageShouldExistWhenTechnologyCreated(int languageId)
        {
            Language? result = await _languageRepository.GetAsync(lang => lang.Id == languageId);

            if (result == null)
                throw new BusinessException("LanguageId does not exist.");
        }
    }
}
