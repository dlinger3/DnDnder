using Tavern.Constants;

namespace Tavern.Models
{
    public class Util
    {
        public static string[] getAllCharacterImages()
        {
            return new string[]
                {
                    CharacterImages.img1,
                    CharacterImages.img2,
                    CharacterImages.img3,
                    CharacterImages.img4,
                    CharacterImages.img5,
                    CharacterImages.img6,
                    CharacterImages.img7,
                    CharacterImages.img8,
                    CharacterImages.img9,
                    CharacterImages.img10,
                    CharacterImages.img11,
                    CharacterImages.img12,
                    CharacterImages.img13
                }; 
        }
        public static string getCharacterImageById(int id)
        {
            return CharacterImages.CharacterImgMap[id];
        }

        public static string[] getAllCampaignImages()
        {
            return new string[]
                {
                    CampaignImages.img1,
                    CampaignImages.img2,
                    CampaignImages.img3,
                    CampaignImages.img4,
                    CampaignImages.img5,
                    CampaignImages.img6,
                    CampaignImages.img7,
                    CampaignImages.img8,
                    CampaignImages.img9,
                    CampaignImages.img10,
                    CampaignImages.img11,
                    CampaignImages.img12,
                    CampaignImages.img13
                };
        }
        public static string getCampaignImageById(int id)
        {
            return CampaignImages.CampaignImgMap[id];
        }
    }
}
