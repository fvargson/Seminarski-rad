using Poke.Data.Model;
namespace Poke.MVC.Extensions
{
    public class CartItem
    {
        public PokeProduct? Product;
        public PokeBundle? Bundle;
        public int Quantity;
        public int GetTotal()
        {
            if(Product != null)
            {
                return Product.Price * Quantity;
            } else if(Bundle != null)
            {
                return Bundle.Price * Quantity;
            } else
            {
                return 0;
            }
        }
    }
}