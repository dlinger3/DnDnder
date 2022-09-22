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
        public string name { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int level { get; set; }
        [Required]
        public string race { get; set; }
        [Required]
        public string alignment { get; set; }
        [MaxLength(5000)]
        public string bio { get; set; }
    }
}
