// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Route("[controller]")]
   
    public class IdentityController : ControllerBase
    {
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
            JObject json = new JObject();
            json.Add("id", id);
            json.Add("account_id", aid);
            json.Add("balance_available", ba);
            json.Add("balance_unavailable", bna);
            return new JsonResult(json);
        }
    }
}