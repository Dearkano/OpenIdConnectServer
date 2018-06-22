using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<FundAccount> FundAccounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<SecuritiesAccount> SecuritiesAccounts { get; set; }
    }
}
