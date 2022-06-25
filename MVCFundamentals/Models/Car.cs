using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFundamentals.Models
{
    public class Car
    {
        //gerekli attiributeler iel veri tanımlaması
        public int Id { get; set; }

        [Required,  MinLength(3), MaxLength(15), Display(Name = "Model Name")]
        public string? ModelName { get; set; }
        [Required]
        public string? Color { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }

    }
}
