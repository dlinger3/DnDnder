using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Tavern.Models
{
    public class ListingChatGroup
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("CampaignListing")]
        public int? CampaignListingID { get; set; }
        public CampaignListing? CampaignListing { get; set; }
      
        [ForeignKey("message")]
        public int? MessageID { get; set; }
        public Message? message { get; set; }
    }

}
