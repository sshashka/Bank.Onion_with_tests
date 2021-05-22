using System.ComponentModel.DataAnnotations;

namespace Bank.Orchestrators.Banks
{
    public class Bank
    {
        [Required]
        public int Id { get; set; }
        [MinLength(1)]
        [MaxLength(255)]
        public string Location { get; set; }
        public string Head { get; set; }
        [Range(1,500)]
        public int Count { get; set; }
    }
}