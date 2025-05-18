using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Definitions.Userprofile;
using Application.Constants.Commons;
using Application.MappingProfile;
using System.Xml;

namespace Application.Features.Implementations.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserProfileService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetUserByIdAsync(string id)
        {
            var user = await _context.Set<ProfileUser>().FindAsync(id);
            return user == null ? null : _mapper.Map<UserProfileDto>(user);
        }

        public async Task<List<UserProfileDto>> GetAllUsersAsync()
        {
            var users = await _context.Set<ProfileUser>().ToListAsync();
            return _mapper.Map<List<UserProfileDto>>(users);
        }
        public async Task CreateUserAsync(UserProfileDto userProfileDto)
        {
            try
            {
                if (userProfileDto == null)
                    throw new ArgumentNullException(nameof(userProfileDto));

                var entity = _mapper.Map<ProfileUser>(userProfileDto);

                if (_context == null)
                    throw new InvalidOperationException("DbContext مقداردهی نشده است.");

                await _context.Set<ProfileUser>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw; 
            }
        }
        

        public async Task UpdateUserAsync(string id, UserProfileDto userProfileDto)
        {
            var entity = await _context.Set<ProfileUser>().FindAsync(id);
            if (entity == null) return;

            _mapper.Map(userProfileDto, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            var entity = await _context.Set<ProfileUser>().FindAsync(id);
            if (entity == null) return;

            _context.Set<ProfileUser>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserProfileDto> GetByIdAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("شناسه نامعتبر است.");
                }

               
                var entity = await _context.Set<ProfileUser>().FindAsync(id);

                
                if (entity == null)
                {
                    throw new KeyNotFoundException("اطلاعاتی با این شناسه یافت نشد.");
                }

                
                return new UserProfileDto
                {
                    Id = entity.Id,
                   
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطا در دریافت اطلاعات: {ex.Message}");
                throw;
            }
        }

    }


}
