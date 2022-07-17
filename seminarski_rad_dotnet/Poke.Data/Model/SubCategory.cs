using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Data.Model
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [ForeignKey("PokeCategoryId")]
        public PokeCategory PokeCategory { get; set; }
        public int PokeCategoryId { get; set; }
    }
}