using System.ComponentModel.DataAnnotations;

namespace DnDnder.Models
{
    public class campaign
    {
        public int Id { get; set; }
        //Name given to a campaign by user
        [Required]
        public string campaignName { get; set; }

        //Name of a camapign world
        public string worldName { get; set; }

        //Variable that can contain details to provide user details on a campaign
        [MaxLength (5000)]
        public string details { get; set; }
    }
}
