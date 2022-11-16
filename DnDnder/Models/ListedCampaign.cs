namespace Tavern.Models
{
    public class ListedCampaign
    {
        public ListedCampaign(int campainID, bool isListed, int listingID)
        {
            CampaignID = campainID;
            IsListed = isListed;
            ListingID = listingID;
        }
        public int CampaignID { get; }
        public bool IsListed { get; }
        public int ListingID { get; }
    }
}
