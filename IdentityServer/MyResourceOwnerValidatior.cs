using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickstartIdentityServer.Data;
using Sakura.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickstartIdentityServer
{
    public class MyResourceOwnerValidatior : IResourceOwnerPasswordValidator
    {
        private MyDbContext dbContext;

        public MyResourceOwnerValidatior(MyDbContext db)
        {
            dbContext = db;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;
            var user = await (from i in dbContext.FundAccounts
                              where i.Id == userName && i.Password == password
                              select i).FirstOrDefaultAsync();
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), "IS", DateTime.Now, new List<Claim> {

                    new Claim("id",user.Id.ToString()),
                    new Claim("account_id",user.AccountId.ToString()),
                    new Claim("balance_available",user.BalanceAvailable.ToString()),
                    new Claim("balance_unavailable",user.BalanceUnAvailable.ToString()) });

            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
            }
        }
    }
}
