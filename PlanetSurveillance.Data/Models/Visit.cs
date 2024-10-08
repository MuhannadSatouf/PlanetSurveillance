using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanetSurveillance.Data.Models
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitId { get; set; }
        public int PersonId { get; set; }
        public int PlanetId { get; set; }
        public DateTime DateOfVisit { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [ForeignKey("PlanetId")]
        public Planet Planet { get; set; }
    }
}
