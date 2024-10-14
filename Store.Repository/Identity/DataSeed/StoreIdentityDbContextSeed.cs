using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Identity.DataSeed
{
    public static class StoreIdentityDbContextSeed
    {

        public async static Task SeedAppUserAsync(UserManager<AppUser> userManager)
        {
           if(userManager.Users.Count()==0)
            {
                var user = new AppUser()
                {
                    Email = "MuhammedBika22@gmail.com",
                    DisplayName = "Muhammed ABdel Raheem",
                    UserName = "Muhammed.AbdelRaheem",
                    PhoneNumber = "01027268605",
                    Address = new Address()
                    {
                        Fname = "Muhammed",
                        Lname = "Abdel Raheem",
                        City = "Alexandria",
                        Country = "Egypt",
                        Street = "Miame"

                    }
                };

                await userManager.CreateAsync(user, "P@ssW0rd");

            }
        }
    }
}
