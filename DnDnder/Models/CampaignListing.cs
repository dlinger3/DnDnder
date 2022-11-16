using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Tavern.Models
{
    public class CampaignListing
    {
        public int Id { get; set; }

        ////Forms a M:M relationship between Characters and CampaignListings
        //public ICollection<Character>? Players { get; set; }

        //Foreign key for Campaign that is associated with this CampaignListing
        [ForeignKey("Campaign")]
        public int? CampaignId { get; set; }
        public virtual Campaign? Campaign { get; set; }

        //User that the CampaignListing is associated with
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public bool UserIsInCampaign(Dictionary<string, int> PlayerListingsMap, int? ListingID, string? UserID)
        {
            string key = UserID + "--" + ListingID;
            if (PlayerListingsMap.ContainsKey(key))
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
