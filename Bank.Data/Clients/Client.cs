using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Data.Clients
{
    [Table("Client")]
    public class Client
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("second_name")]
        [Required]
        public string SecondName { get; set; }
        [Column("sum")]
        [Required]
        public int Sum { get; set; }
        [ForeignKey("Bank")] 
        [Column("bank_id")]
        public int BankId { get; set; }
        public Banks.Bank Bank { get; set; }
    }
}
