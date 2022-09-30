using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();
            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();
            CreateMap<IPaginate<GitHubProfile>, GitHubProfileListModel>().ReverseMap();

            CreateMap<GitHubProfile, GitHubProfileListDto>()
                .ForMember(c => c.UserId, opt => opt.MapFrom(c => c.User.Id))
                .ReverseMap();
        }        
    }
}
