using System.ComponentModel.DataAnnotations;

namespace Poke.Data.Model
{
    public class PokeBundle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? PackDescription { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}