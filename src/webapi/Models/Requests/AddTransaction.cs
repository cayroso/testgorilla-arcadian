using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Requests
{
    public class AddTransaction
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Cost { get; set; }
    }
}
