using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Data.Model
{
    public class PokeProductCategory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PokeProductId")]
        public PokeProduct PokeProduct { get; set; }
        public int PokeProductId { get; set; }
        
        [ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}