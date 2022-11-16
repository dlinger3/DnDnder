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

        public bool UserIsDM(string id, int? CharacterID)
        {
            //When a campaign is listed, the DM is added to the CampaignCharacters table in order
            //to gain full access to the listing. The CharacterID field is set to -1 to indicate this user
            //is the DM of the campaign
            if(CharacterID == -1 && this.AppUserID.Equals(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
