using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tavern.Models
{
    public class CampaignCharacters
    {
        public int Id { get; set; }
        [ForeignKey("CampaignListing")]
        public int? CampaignListingID { get; set; }
        public virtual CampaignListing? CampaignListing { get; set; }
        [ForeignKey("Character")]
        public int? CharacterID { get; set; }
        public virtual Character? Character { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }

    }
}
