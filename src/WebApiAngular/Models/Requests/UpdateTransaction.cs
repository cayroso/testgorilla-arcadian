using System.ComponentModel.DataAnnotations;

namespace WebApiAngular.Models.Requests
{
    public class UpdateTransaction
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Cost { get; set; }
    }
}
