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
            try
            {
              
                if (string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.Password))
                {
                    throw new BusinessException(ErrorType.InvalidCredentials);
                }

                var user = await _userManager.FindByNameAsync(request.PhoneNumber);
                if (user == null)
                {
                    throw new BusinessException(ErrorType.InvalidPhoneNumber);
                }

               
                if (!user.IsActive)
                {
                    throw new BusinessException(ErrorType.AccountInactive);
                }

               
                if (string.IsNullOrEmpty(user.UserName))
                {
                    throw new BusinessException(ErrorType.InvalidCredentials);
                }

                
                Console.WriteLine($"UserName: {user.UserName}");

             
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, lockoutOnFailure: true);

              
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        throw new BusinessException(ErrorType.AccountLocked);
                    }
                    else if (result.IsNotAllowed)
                    {
                        throw new BusinessException(ErrorType.AccountNotAllowed);
                    }

                    throw new BusinessException(ErrorType.InvalidCredentials);
                }

               
                return new AuthResponse
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            catch (BusinessException ex)
            {
                Console.WriteLine($"BusinessException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
                throw new BusinessException(ErrorType.InvalidCredentials);
            }
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
