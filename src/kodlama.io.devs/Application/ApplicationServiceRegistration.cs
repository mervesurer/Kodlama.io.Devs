﻿using Application.Features.Authorization.Rules;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.Languages.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Technologies.Rules;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.AuthService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<LanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<GitHubProfileBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();
            services.AddScoped<UserOperationClaimBusinessRules>();

            services.AddScoped<IAuthService, AuthManager>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }    
}
