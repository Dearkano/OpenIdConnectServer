using IdentityServer4.Models;
using IdentityServer4.Services;
using QuickstartIdentityServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer
{
    public class myIProfileService : IProfileService
    {
        public MyDbContext dbContext;
        public myIProfileService(MyDbContext db)
        {
            dbContext = db;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.FindFirst("id").Value;
            var accountId = context.Subject.FindFirst("account_id").Value;
            var ba = context.Subject.FindFirst("balance_available").Value;
            var bna = context.Subject.FindFirst("balance_unavailable").Value;
            context.IssuedClaims.Add(new System.Security.Claims.Claim("id", id));
            context.IssuedClaims.Add(new System.Security.Claims.Claim("account_id", accountId));
            context.IssuedClaims.Add(new System.Security.Claims.Claim("balance_available", ba));
            context.IssuedClaims.Add(new System.Security.Claims.Claim("balance_unavailable", bna));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //throw new NotImplementedException();
        }


    }
}
