using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore21.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Senha123$";

        public static async void EnsurePopulated( IApplicationBuilder app )
        {
            UserManager<IdentityUser> userManager =
                app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync("Admin");
            if (user == null)
            {
                user = new IdentityUser( "Admin" );
                user.Email = "admin@example.com";
                try
                {
                    await userManager.CreateAsync(user, adminPassword);
                }                
                catch( Exception ex )
                {
                    throw ex;
                }
            }
        }
    }
}
