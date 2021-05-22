using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bank.Core.Clients;

namespace Bank.Data.Banks
{
    [Table("Bank-onion")]
    public class Bank
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("head")]
        public string Head { get; set; }
        [Column("countofworkers")]
        public int Count { get; set; }

        public virtual ICollection<Core.Clients.Clients> Clients { get; set; }
    }
}
