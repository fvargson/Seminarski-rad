using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Data.Model
{
    public class PokeBundleProduct
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("PokeProductId")]
        public PokeProduct PokeProduct { get; set; }
        public int PokeProductId { get; set; }

        [ForeignKey("PokeBundleId")]
        public PokeBundle PokeBundle { get; set; }
        public int PokeBundleId { get; set; }
    }
}