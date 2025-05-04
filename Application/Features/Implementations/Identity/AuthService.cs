using Application.Constants.Identity;
using Application.Features.Definitions.Identity;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Application.Features.Implementations.Identity
{
    //public class AuthService : IAuthService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly IDisposableCodeService _disposableCodeService;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IRoleService _roleService;
    //    private readonly ITokenService _tokenService;

    //    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor
    //         , IRoleService roleService, ITokenService tokenService, IDisposableCodeService disposableCodeService)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _httpContextAccessor = httpContextAccessor;
    //        _disposableCodeService = disposableCodeService;
    //        _tokenService = tokenService;
    //        _roleService = roleService;
    //    }

    //    public async Task<RegistrationResponse> Register(RegisterationRequest request)
    //    {
    //        var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
    //        if (existingUser != null)
    //        {
    //            throw new BusinessException(ErrorType.PhoneNumberAlreadyExists);
    //        }

    //        await _disposableCodeService.UpdateDisposableCodeAsUsed(new Dtos.Messengers.Sms.DisposableCodeDto
    //        {
    //            PhoneNumber = request.PhoneNumber,
    //            DisposableCode = request.DisposableCode,
    //        }, true);

    //        var user = new ApplicationUser
    //        {
    //            FirstName = request.FirstName,
    //            LastName = request.LastName,
    //            UserName = request.PhoneNumber,
    //            Email = $"{Guid.NewGuid()}@example.com",
    //            EmailConfirmed = true,
    //            SecurityStamp = Guid.NewGuid().ToString(),
    //            ConcurrencyStamp = Guid.NewGuid().ToString(),
    //            PhoneNumber = request.PhoneNumber,
    //            PhoneNumberConfirmed = true,
    //        };

    //        var result = await _userManager.CreateAsync(user, request.Password);

    //        if (result.Succeeded)
    //        {
    //            await _roleService.AssignRoleToUser(user, request.roleName);
    //            return new RegistrationResponse() { UserId = user.Id };
    //        }
    //        else
    //        {
    //            throw new Exception($"{result.Errors}");
    //        }
    //    }
    //    public async Task<AuthResponse> Login(AuthRequest request)
    //    {
    //        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
    //        if (user == null)
    //        {
    //            throw new BusinessException(ErrorType.InvalidPhoneNumber);
    //        }
    //        if (!user.IsActive)
    //        {
    //            throw new BusinessException(ErrorType.AccountInactive);
    //        }

    //        if (request.IsOtpLogin)
    //        {
    //            await _disposableCodeService.UpdateDisposableCodeAsUsed(new Dtos.Messengers.Sms.DisposableCodeDto
    //            {
    //                PhoneNumber = request.PhoneNumber,
    //                DisposableCode = request.DisposableCode,
    //            }, true);
    //        }
    //        else
    //        {
    //            var result = await _signInManager.PasswordSignInAsync(user.UserName ?? "", request.Password, false, lockoutOnFailure: false);
    //            if (!result.Succeeded)
    //            {
    //                throw new BusinessException(ErrorType.InvalidCredentials);
    //            }
    //        }

    //        JwtSecurityToken jwtSecurityToken = await _tokenService.GenerateJwtToken(user);
    //        AuthResponse response = new AuthResponse()
    //        {
    //            Id = user.Id,
    //            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
    //            FirstName = user.FirstName,
    //            LastName = user.LastName,
    //            UserName = user.UserName ?? "",
    //        };

    //        return response;
    //    }
    //    public async Task Logout()
    //    {
    //        await _signInManager.SignOutAsync();
    //        //_httpContextAccessor.HttpContext.Response.Cookies.Delete("YourCookieName");
    //    }
    //    public async Task<string> GetCurrentUserId()
    //    {
    //        var result = await Task.Run(() =>
    //        {
    //            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimTypes.Uid);
    //        });

    //        return result ?? "";
    //    }

    //    #region Register
    //    //public async Task<RegistrationResponse> Register(RegisterationRequest request)
    //    //{
    //    //    var existingUser = await _userManager.FindByNameAsync(request.UserName);
    //    //    if (existingUser != null)
    //    //    {
    //    //        throw new Exception($"user name '{request.UserName}' already exists.");
    //    //    }

    //    //    var user = new ApplicationUser
    //    //    {
    //    //        Email = request.Email,
    //    //        FirstName = request.FirstName,
    //    //        LastName = request.LastName,
    //    //        UserName = request.UserName,
    //    //        EmailConfirmed = true
    //    //    };


    //    //    var existingEmail = await _userManager.FindByEmailAsync(request.Email);
    //    //    if (existingEmail == null)
    //    //    {
    //    //        var result = await _userManager.CreateAsync(user, request.Password);

    //    //        if (result.Succeeded)
    //    //        {
    //    //            await _userManager.AddToRoleAsync(user, "Employee");
    //    //            return new RegistrationResponse() { UserId = user.Id };
    //    //        }
    //    //        else
    //    //        {
    //    //            throw new Exception($"{result.Errors}");
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        throw new Exception($"Email '{request.Email}' already exists.");
    //    //    }
    //    //}

    //    //public async Task<AuthResponse> Login(AuthRequest request)
    //    //{
    //    //    var user = await _userManager.FindByEmailAsync(request.Email);
    //    //    if (user == null)
    //    //    {
    //    //        throw new Exception($"user with {request.Email} not fount.");
    //    //    }

    //    //    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

    //    //    if (!result.Succeeded)
    //    //    {
    //    //        throw new Exception($"credentials for {request.Email} arent valid.");
    //    //    }

    //    //    JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

    //    //    AuthResponse response = new AuthResponse()
    //    //    {
    //    //        Id = user.Id,
    //    //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
    //    //        Email = user.Email,
    //    //        UserName = user.UserName,
    //    //    };

    //    //    return response;
    //    //}
    //    #endregion

    //}
}
