using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickstartIdentityServer.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickstartIdentityServer
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public MyDbContext dbContext;
        public HomeController(MyDbContext myDbContext)
        {
            dbContext = myDbContext;
        }
        [HttpGet]
        public async Task<string[]> Index()
        {
            var tbs = dbContext.FundAccounts;
            var data = await (from i in tbs select i.Id).ToArrayAsync();
            return data;
        }
    }
}
