using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanetSurveillance.Data.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string BirthYear { get; set; }
        public string Homeworld { get; set; }
        public List<string> Films { get; set; } = new List<string>();
        public List<string> Species { get; set; } = new List<string>();
        public List<string> Vehicles { get; set; } = new List<string>();
        public List<string> Starships { get; set; } = new List<string>();
        public string Created { get; set; }
        public string Edited { get; set; }
        public string Url { get; set; }
        [Required]
        public string SWAPIId { get; set; }
    }
}
