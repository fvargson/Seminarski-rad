using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Data.Model
{
    public class PokeProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int PokemonId { get; set; }
        public bool InBundle { get; set; }
    }
}