using Core.DTOs;
using Microsoft.AspNetCore.Identity;
using UIupdated.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace UIupdated.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<bool> AssignRoleAsync(string userId, string roleName);
        Task<bool> RemoveRoleAsync(string userId, string roleName);
        Task<bool> RemoveRoleByIdAsync(string userId, string roleId);
        Task<IdentityRole?> GetRoleByNameAsync(string roleName);
        Task<bool> AssignRoleAdminAsync(string userId);
        Task<bool> AssignRoleBossAsync(string userId);


    }



    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserService> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserService> logger,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<bool> AssignRoleAdminAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return false;

            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
                return false;

            var alreadyInRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (alreadyInRole)
                return true;

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result.Succeeded;
        }
        public async Task<bool> AssignRoleBossAsync(string userId)
        {
            return await AssignRoleAsync(userId, "Boss");
        }

        public async Task<bool> RemoveRoleByIdAsync(string userId, string roleId)
        {
            _logger.LogInformation("\n\n Trying to remove role {RoleId} from user {UserId} \n\n", roleId, userId);

            var userRole = await _dbContext.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)
                return false;

            _dbContext.UserRoles.Remove(userRole);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<IdentityRole?> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<bool> RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return true;
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
                    IsWorker = roles.Contains("Worker"),
                    Roles = roles.ToList()
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
