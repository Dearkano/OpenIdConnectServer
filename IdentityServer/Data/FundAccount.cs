﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuickstartIdentityServer.Data
{

    [Table("funds_account")]
    public class FundAccount
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [ForeignKey("account_id")]
        [Column("account_id")]
        public string AccountId{ get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("balance_available")]
        public decimal BalanceAvailable { get; set; }

        [Column("balance_unavailable")]
        public decimal BalanceUnAvailable { get; set; }

        [Column("account_status")]
        public string AccountStatus { get; set; }
    }

}
