using Poke.Data.Model;

namespace Poke.Data.Interface
{
    public interface IApplicationRepo
    {
        public IEnumerable<SubCategory> GetSubCategories(int id);
        public SubCategory GetSubCategory(int id);
        public SubCategory CreateSubCategory(SubCategory subCategory);
        public SubCategory EditSubCategory(int id, SubCategory subCategory);
        public SubCategory RemoveSubCategory(int id);
        public IEnumerable<PokeCategory> GetPokeCategories();
        public IEnumerable<PokeProductCategory> GetPokeProductCategories();
        public IEnumerable<PokeProduct> GetPokeProducts();
        public PokeProduct? GetPokeProductById(int id);
        public PokeProduct CreatePokeProduct(PokeProduct pokeProduct);
        public PokeProduct EditPokeProduct(int id, PokeProduct pokeProduct);
        public PokeProduct RemovePokeProduct(int id);
        public IEnumerable<PokeBundleProduct> GetPokeBundleProducts();
        public IEnumerable<PokeBundleProduct> GetPokeBundleProduct(int id);
        public PokeBundleProduct CreateBundleProduct(PokeBundleProduct bundleProduct);
        public IEnumerable<PokeBundle> GetPokeBundles();
        public PokeBundle GetPokeBundle(int id);
        public PokeBundle CreatePokeBundle(PokeBundle pokeBundle, IEnumerable<int> pokemonIds);
        public PokeBundle EditPokeBundle(int id, PokeBundle pokeBundle, IEnumerable<int> pokemonIds);
        public PokeBundle RemovePokeBundle(int id);
        public PokeInvoice CreatePokeInvoice(PokeInvoice pokeInvoice);
        public OrderItem CreateOrderItem(OrderItem orderItem);
        public IEnumerable<PokeInvoice> GetPokeInvoices();
        public PokeInvoice GetPokeInvoice(int id);
        public IEnumerable<OrderItem> GetOrderItems(int id);
    }
}