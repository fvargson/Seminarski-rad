using System.ComponentModel.DataAnnotations;

namespace Poke.Data.Model
{
    public class Avatar
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string UserId { get; set; }
    }
}