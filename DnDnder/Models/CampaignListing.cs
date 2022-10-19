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

        //User that the CampaignListing is associated with
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public bool UserIsInCampaign(string UserID)
        {
            if(Players == null)
            {
                return false;
            }
            else
            {
                foreach (Character character in Players)
                {
                    if (character.AppUserID == UserID)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }

                return false;
            }
            
        }

    }
}
