using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bank.Core.Clients;

namespace Bank.Data.Bank
{
    [Table("banks")]
    public class Bank
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("head")]
        public string Head { get; set; }
        [Column("countofworkers")]
        public int CountOfWorkers { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
    }
}