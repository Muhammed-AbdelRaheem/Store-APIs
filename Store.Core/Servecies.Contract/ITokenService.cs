using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Servecies.Contract
{

    public interface ITokenService
    {

        public Task<string> CreateTokenAsync(AppUser user ,UserManager<AppUser> userManager);
    }
}
 