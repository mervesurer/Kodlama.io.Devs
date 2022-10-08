﻿using Application.Features.OperationClaims.Commands.CreateOpreationClaim;
using Application.Features.OperationClaims.Commands.UpdateOpreationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Model;
using Application.Features.Technologies.Dtos;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Model;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, UpdatedUserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, DeletedUserOperationClaimDto>().ReverseMap();
            CreateMap<IPaginate<UserOperationClaim>, UserOperationClaimListModel>().ReverseMap();
            CreateMap<UserOperationClaim, UserOperationClaimListDto>()
                .ForMember(c => c.Username, opt => opt.MapFrom(c => c.User.FirstName))
                .ForMember(c => c.OperationClaimName, opt => opt.MapFrom(c => c.OperationClaim.Name))
                .ReverseMap();
        }
    }
}