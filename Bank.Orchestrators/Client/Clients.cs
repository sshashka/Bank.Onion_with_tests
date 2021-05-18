using System.ComponentModel.DataAnnotations;

namespace Bank.Orchestrators.Client
{
    public class Clients
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        [Required]
        public int Sum { get; set; }
    }
}