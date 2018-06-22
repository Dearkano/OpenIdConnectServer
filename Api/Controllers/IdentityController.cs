// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json.Linq;
using Api.Data;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("[controller]")]
   
    public class IdentityController : ControllerBase
    {
        public MyDbContext dbContext;
        public IdentityController(MyDbContext db)
        {
            dbContext = db;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var claims = (from c in User.Claims select new { c.Type,c.Value }).ToArray();
            string id="", aid="";
            double ba=0, bna=0;

            for (int i = 0; i < claims.Length; i++)
            {
                if (claims[i].Type == "id")
                {
                    id = claims[i].Value;
                }else if (claims[i].Type == "account_id")
                {
                    aid = claims[i].Value;
                }else if (claims[i].Type == "balance_available")
                {
                    ba = Convert.ToDouble(claims[i].Value);
                }else if (claims[i].Type == "balance_unavailable")
                {
                    bna = Convert.ToDouble(claims[i].Value);
                }
            }
            var accountIm = await (from i in dbContext.SecuritiesAccounts where i.Id == aid select i).FirstOrDefaultAsync();
            var aType = accountIm.AccountType;
            var pId = accountIm.PersonId;
            var person = await (from i in dbContext.Persons where i.PersonId == pId select i).FirstOrDefaultAsync();
            string jsonData = JsonConvert.SerializeObject(person);
            JObject json = new JObject();
            json.Add("id", id);
            json.Add("account_id", aid);
            json.Add("balance_available", ba);
            json.Add("balance_unavailable", bna);
            json.Add("account_type", aType);
            json.Add("person_id", person.PersonId);
            json.Add("name", person.Name);
            json.Add("sex", person.Sex);
            json.Add("phone_number", person.PhoneNumber);
            json.Add("address", person.Address);
            json.Add("email", person.Email);
            return new JsonResult(json);
        }
    }
}