using System.ComponentModel.DataAnnotations;

namespace DnDnder.Models
{
    public class Character
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public String name { get; set; }
        [Required]
        public String Class { get; set; }
        [Required]
        public int level { get; set; }
        [Required]
        public String race { get; set; }
        [Required]
        public String alignment { get; set; }
    }
}
