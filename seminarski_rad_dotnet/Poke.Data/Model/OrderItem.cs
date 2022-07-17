using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poke.Data.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("OrderId")]
        public PokeInvoice Order { get; set; }
        public int OrderId { get; set; }
        public string ProductTitle { get; set; }
        public int? ProductId { get; set; }
        public int? BundleId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public string? ProductDescription { get; set; }
    }
}