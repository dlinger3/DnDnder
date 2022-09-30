using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tavern.Models
{
    public class CampaignListing
    {
        public int Id { get; set; }

        //Collection of players in the campaign
        public ICollection<Character>? Players { get; set; }

        //Foreign key for Campaign that is associated with this CampaignListing
        [ForeignKey("Campaign")]
        public int? CampaignId { get; set; }
        public virtual Campaign? Campaign { get; set; }

    }
}
