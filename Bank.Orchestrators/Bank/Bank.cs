using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Orchestrators.Bank
{
    public class Bank
    {
        [Required]
        public int Id { get; set; }
        [MinLength(1)]
        [MaxLength(255)]
        public string Location { get; set; }
        [MinLength(1)]
        [MaxLength(255)]
        public string Head { get; set; }
        [Range(1,500)]
        public int CountOfWorkers { get; set; }
    }
}