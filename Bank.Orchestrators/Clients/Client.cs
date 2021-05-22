using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bank.Orchestrators.Clients
{
    [Table("Client")]
    public class Client
    {
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("location")]
        public string Name { get; set; }
        [Required]
        [Column("second_name")]
        public string SecondName { get; set; }
        [Required]
        [Column("sum")]
        public int Sum { get; set; }
        
        public int BankId { get; set; }
    }
}
