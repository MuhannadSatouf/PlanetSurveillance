using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlanetSurveillance.Data.Models
{
    public class Planet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanetId { get; set; }               
        public string Name { get; set; }          
        public string RotationPeriod { get; set; }   
        public string OrbitalPeriod { get; set; }  
        public string Diameter { get; set; }        
        public string Climate { get; set; }     
        public string Gravity { get; set; }      
        public string Terrain { get; set; }       
        public string SurfaceWater { get; set; }    
        public string Population { get; set; }

        public List<string> Films { get; set; } = new List<string>();
        public List<string> Residents { get; set; } = new List<string>();

        // Metadata fields
        public string Created { get; set; }     
        public string Edited { get; set; }         
        public string Url { get; set; }
        [Required]
        public string SWAPIId { get; set; }         
    }
}
