using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorization.Dtos
{
    public class AuthRegisterDto
    {
        public AccessToken AccessToken { get; set; }
    }
}
