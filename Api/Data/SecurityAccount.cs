using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    [Table("securities_account")]
    public class SecuritiesAccount
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("person_id")]
        public string PersonId { get; set; }

        [Column("account_type")]
        public string AccountType { get; set; }

        [Column("account_status")]
        public string AccountStatus { get; set; }
    }
}
