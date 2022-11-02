using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Tavern.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? MessageText { get; set; }

        public string? SentBy { get; set; }

        [Required]
        [ForeignKey("CampaignListing")]
        public int? CampaignListingID { get; set; }
        public CampaignListing? campaignListing { get; set; }

        //public string SocketId  { get; set; }

        //[Required]
        //[ForeignKey("ListingChatGroup")]
        //public int? ListingChapGroupID { get; set; }
        //public ListingChatGroup? ListingChatGroup { get; set; }

    }
}
