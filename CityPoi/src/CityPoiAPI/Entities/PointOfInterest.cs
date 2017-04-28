using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityPoiAPI.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
          
        [Required]
        public string Address { get; set; }

        /*Commentaire sur les regex:
         * Avec Bogus, normalement, pour la latitude/longitude,
         * il y a 4 décimales. Mais parfois, Bogus décide d'en mettre entre 1 et 10.
         */
        [Required]
        [RegularExpression("-?((1[0-7]|[0-9])?[0-9][,.][0-9]+|180[,.]0+)")]
        public string Longitude { get; set; }

        [Required]
        [RegularExpression("-?([0-8]?[0-9][,.][0-9]+|90[,.]0+)")]
        public string Latitude { get; set; }

        [ForeignKey("CityName")]
        [Required]
        public string CityName { get; set; }
    }
}
