using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proprette.Domain.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [MaxLength(50)]
        public string LocationName { get; set; }

        public Location(string locationName)
        {
            LocationName = locationName;
        }
    }
}
