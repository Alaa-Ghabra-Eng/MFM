﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using System.Security.Claims;

namespace MFM.Models {
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.GivenName, user.FirstName)
          });
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.Surname, user.LastName),
          });
            }
            return principal;
        }
    }    
}

