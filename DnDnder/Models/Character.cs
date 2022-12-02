using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tavern.Models
{
    public class Character
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public string Alignment { get; set; }
        [MaxLength(5000)]
        public string Bio { get; set; }

        public int CharacterImg { get; set; }

        //Foreign key and AppUser that is associated with this Character
        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        //Forms a M:M relationship between Characters and CampaignListings
        //public ICollection<CampaignListing>? PlayerCampaigns { get; set; }
    }
}
