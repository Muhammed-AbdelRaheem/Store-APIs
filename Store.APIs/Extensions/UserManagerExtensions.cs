using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.APIs.Errors;
using Store.Core.Entities.Identity;
using System.Security.Claims;

namespace Store.APIs.Extensions
{
    public static class UserManagerExtensions
    {

        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager ,ClaimsPrincipal User)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return null;

          var user= await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email== userEmail);
            if (user is null) return null;

            return user;

        }
    }
}
