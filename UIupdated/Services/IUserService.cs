using Core.DTOs;
using Microsoft.AspNetCore.Identity;
using UIupdated.Data;
using Microsoft.Extensions.Logging;

namespace UIupdated.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<bool> AssignRoleAsync(string userId, string roleName);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserService> _logger;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FirstName = "",
                    LastName = "",
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    IsWorker = roles.Contains("Worker")
                });
            }
            return userDtos;
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                Id = user.Id,
                FirstName = "",
                LastName = "",
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                IsWorker = roles.Contains("Worker")
            };
        }

        public async Task<bool> AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found when assigning role {RoleName}", userId, roleName);
                return false;
            }

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogWarning("Role {RoleName} does not exist. Creating...", roleName);
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError("FAILED to assign role {RoleName} to user {UserId}: {ErrorCode} - {ErrorDesc}", roleName, userId, error.Code, error.Description);
                    }
                    return false;
                }
                else
                {
                    _logger.LogInformation("SUCCESS: Role {RoleName} assigned to user {UserId}", roleName, userId);
                }

                _logger.LogInformation("Role {RoleName} assigned to user {UserId}", roleName, userId);
                return true;
            }
            else
            {
                _logger.LogInformation("User {UserId} already has role {RoleName}", userId, roleName);
                return true;
            }
        }
    }
}
