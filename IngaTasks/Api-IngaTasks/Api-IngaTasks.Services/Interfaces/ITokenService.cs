using Api_IngaTasks.Application.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services.Interfaces
{
    public interface ITokenService
    {
        public String GerarToken(ApplicationUser user);
    }
}
