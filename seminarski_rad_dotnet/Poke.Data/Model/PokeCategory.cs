using System.ComponentModel.DataAnnotations;

namespace Poke.Data.Model
{
    public class PokeCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}