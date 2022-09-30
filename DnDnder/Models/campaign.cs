using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tavern.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        //Name given to a campaign by user
        [Required]
        public string CampaignName { get; set; }

        //Name of a camapign world
        public string WorldName { get; set; }

        //Variable that can contain details to provide user details on a campaign
        [MaxLength(5000)]
        public string Details { get; set; }

        //Foreign key and AppUser that is associated with this Campaign
        [ForeignKey("AppUser")]
        public string? AppUserID  { get; set; }
        public virtual AppUser? AppUser { get; set; }

    }
}