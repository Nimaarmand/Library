
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Definitions.Identity
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user);
    }
}
