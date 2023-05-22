using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proprete.Data.Tables
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required]
        [MaxLength(50)]
        public string LocationName { get; set; } 

        public Location(string locationName)
        {
            LocationName = locationName;
        }
    }
}
