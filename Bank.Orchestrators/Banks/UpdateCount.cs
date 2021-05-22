using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Orchestrators.Banks
{
    public class UpdateCount
    {
        [Required] 
        public int Count { get; set; }
    }
}
