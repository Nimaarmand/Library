using Application.Features.Definitions.Identity;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Implementations.Identity
{
    //public class TokenService : ITokenService
    //{
    //    //private readonly JwtSettings _jwtSettings;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly RoleManager<ApplicationRole> _roleManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;

    //    public TokenService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, /*IOptions<JwtSettings> jwtSettings*/,
    //        SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    //    {
    //        _userManager = userManager;
    //        _roleManager = roleManager;
    //        _jwtSettings = jwtSettings.Value;
    //        _signInManager = signInManager;
    //    }

    //    public async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
    //    {
    //        var now = DateTime.UtcNow;

    //        var userClaims = await _userManager.GetClaimsAsync(user);
    //        var roles = await _userManager.GetRolesAsync(user);

    //        var roleClaims = new List<Claim>();

    //        for (int i = 0; i < roles.Count; i++)
    //        {
    //            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
    //        }

    //        var claims = new[]
    //        {
    //            new Claim(JwtRegisteredClaimNames.Sub,user.UserName ?? ""),
    //            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
    //            //new Claim(JwtRegisteredClaimNames.Email,user.Email ?? ""),
    //            new Claim(JwtRegisteredClaimNames.PhoneNumber,user.PhoneNumber ?? ""),
    //            new Claim(CustomClaimTypes.Uid,user.Id),
    //        }
    //        .Union(userClaims)
    //        .Union(roleClaims);

    //        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    //        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    //        var jwtSecurityToken = new JwtSecurityToken(
    //        issuer: _jwtSettings.Issuer,
    //            audience: _jwtSettings.Audience,
    //            claims: claims,
    //            notBefore: now,
    //            expires: now.AddMinutes(_jwtSettings.DurationInMinutes),
    //            signingCredentials: signingCredentials);

    //        return jwtSecurityToken;
    //    }



    //}
}
