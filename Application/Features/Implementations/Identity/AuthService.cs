using Application.Constants.Identity;
using Application.Dtos.Identity;
using Application.Dtos.Identity.Authentications;
using Application.Exceptions.BusinessExceptions;
using Application.Features.Definitions.Identity;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Features.Implementations.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RegistrationResponse> Register(RegisterationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (existingUser != null)
            {
                throw new BusinessException(ErrorType.PhoneNumberAlreadyExists);
            }

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.PhoneNumber,
                Email = $"{Guid.NewGuid()}@example.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                throw new BusinessException(ErrorType.InvalidPhoneNumber);
            }
            if (!user.IsActive)
            {
                throw new BusinessException(ErrorType.AccountInactive);
            }
var result = await _signInManager.PasswordSignInAsync(user.UserName ?? "", request.Password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    //throw new BusinessException(ErrorType.InvalidCredentials);
                }

            AuthResponse response = new AuthResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName ?? "",
            };

            return response;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<string> GetCurrentUserId()
        {
            var result = await Task.Run(() =>
            {
                return _httpContextAccessor.HttpContext?.User?.FindFirstValue(CustomClaimTypes.Uid);
            });

            return result ?? "";
        }

        

    }
}
