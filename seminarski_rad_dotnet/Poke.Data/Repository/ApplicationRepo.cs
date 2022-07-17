using Poke.Data.Interface;
using Poke.Data.Model;

namespace Poke.Data.Repository
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IPokemonRepository _poke_repo;
        public ApplicationRepo(ApplicationDbContext context, IPokemonRepository poke_repo)
        {
            _context = context;
            _poke_repo = poke_repo;
        }

        public IEnumerable<SubCategory> GetSubCategories(int id)
        {
            return _context.SubCategories.Where(sc => sc.PokeCategoryId == id).ToList();
        }

        public SubCategory GetSubCategory(int id)
        {
            return _context.SubCategories.Where(sc => sc.Id == id).FirstOrDefault();
        }

        public SubCategory CreateSubCategory(SubCategory subCategory)
        {
            var new_subCategory = _context.SubCategories.Add(subCategory);
            _context.SaveChanges();

            return new_subCategory.Entity;
        }

        public SubCategory EditSubCategory(int id, SubCategory subCategory)
        {
            var new_subCategory = GetSubCategory(id);
            new_subCategory.Name = subCategory.Name;
            new_subCategory.PokeCategoryId = 3;

            _context.SaveChanges();

            return new_subCategory;
        }

        public SubCategory RemoveSubCategory(int id)
        {
            var removed_category = _context.SubCategories.Remove(GetSubCategory(id));
            _context.SaveChanges();

            return removed_category.Entity;
        }

        public IEnumerable<PokeCategory> GetPokeCategories()
        {
            return _context.PokeCategories.ToList();
        }

        public IEnumerable<PokeProductCategory> GetPokeProductCategories()
        {
            return _context.PokeProductCategories.ToList();
        }

        public IEnumerable<PokeProduct> GetPokeProducts()
        {
            return _context.PokeProducts.ToList();
        }

        public PokeProduct? GetPokeProductById(int id)
        {
            return _context.PokeProducts.FirstOrDefault(pp => pp.Id == id);
        }

        public PokeProduct CreatePokeProduct(PokeProduct pokeProduct)
        {
            var new_product = _context.PokeProducts.Add(pokeProduct).Entity;

            _context.SaveChanges();
            
            UpdatePokeCategories(new_product);

            return new_product;
        }

        public PokeProduct EditPokeProduct(int id, PokeProduct new_product)
        {
            var poke = GetPokeProductById(id);

            if(new_product.PokemonId != poke.PokemonId)
            {
                _context.PokeProductCategories.RemoveRange(_context.PokeProductCategories.Where(ppc => ppc.PokeProductId == poke.Id).ToList());
                
                UpdatePokeCategories(new_product);
            }

            poke.Id = id;
            poke.Name = new_product.Name;
            poke.PokemonId = new_product.PokemonId;
            poke.Price = new_product.Price;
            poke.Quantity = new_product.Quantity;

            _context.SaveChanges();

            return poke;
        }

        public PokeProduct RemovePokeProduct(int id)
        {
            var poke = GetPokeProductById(id);
            var pokeProductCategories = GetPokeProductCategories().Where(ppc => ppc.PokeProductId == id).ToList();

            _context.PokeProductCategories.RemoveRange(pokeProductCategories);

            var removed_product = _context.PokeProducts.Remove(poke);
            
            _context.SaveChanges();

            return removed_product.Entity;
        }

        private void UpdatePokeCategories(PokeProduct new_product)
        {
            var gen = _poke_repo.GetPokemonGeneration(new_product.PokemonId);

            _context.PokeProductCategories
                .Add(
                    new PokeProductCategory() 
                    {
                        PokeProductId = new_product.Id,
                        SubCategoryId = _context.SubCategories
                                            .Where(sc => sc.Name == "Gen. " + gen)
                                            .Select(sc => sc.Id)
                                            .FirstOrDefault()
                    }
                );
            
            var pokeType = _poke_repo.GetPokemonType(new_product.PokemonId);

            foreach(var typing in pokeType)
            {
                _context.PokeProductCategories
                    .Add(
                        new PokeProductCategory()
                        {
                            PokeProductId = new_product.Id,
                            SubCategoryId = _context.SubCategories
                                                .Where(sc => sc.Name == typing)
                                                .Select(sc => sc.Id)
                                                .FirstOrDefault()
                        }
                    );
            }

            List<string> other = _context.SubCategories
                                    .Where(sc => sc.PokeCategoryId == 3)
                                    .Select(sc => sc.Name.ToLower())
                                    .ToList();
            
            foreach(var category in other)
            {
                bool is_in;
                switch(category)
                {
                    case "starter": is_in = _poke_repo.IsStarter(new_product.PokemonId, gen); break;
                    case "legendary": is_in = _poke_repo.IsLegendary(new_product.PokemonId); break;
                    case "mythical": is_in = _poke_repo.IsMythical(new_product.PokemonId); break;
                    case "baby": is_in = _poke_repo.IsBaby(new_product.PokemonId); break;
                    default: is_in = false; break;
                }
                if(is_in)
                {
                    _context.PokeProductCategories
                        .Add(
                            new PokeProductCategory()
                            {
                                PokeProductId = new_product.Id,
                                SubCategoryId = _context.SubCategories
                                                    .Where(sc => sc.Name.ToLower() == category)
                                                    .Select(sc => sc.Id)
                                                    .FirstOrDefault()
                            }
                        );
                }
            }

            _context.SaveChanges();
        }

        public IEnumerable<PokeBundleProduct> GetPokeBundleProducts()
        {
            return _context.PokeBundleProducts.ToList();
        }
        
        public IEnumerable<PokeBundleProduct> GetPokeBundleProduct(int bundleId)
        {
            return GetPokeBundleProducts().Where(pbp => pbp.PokeBundleId == bundleId).ToList();
        }
        
        public PokeBundleProduct CreateBundleProduct(PokeBundleProduct bundleProduct)
        {
            var new_bundleProduct = _context.PokeBundleProducts.Add(bundleProduct).Entity;
            
            _context.SaveChanges();

            return new_bundleProduct;
        }

        public IEnumerable<PokeBundle> GetPokeBundles()
        {
            return _context.PokeBundles.ToList();
        }

        public PokeBundle GetPokeBundle(int id)
        {
            return GetPokeBundles().FirstOrDefault(pb => pb.Id == id);
        }

        public PokeBundle CreatePokeBundle(PokeBundle pokeBundle, IEnumerable<int> pokemonIds)
        {
            var new_bundle = _context.PokeBundles.Add(pokeBundle).Entity;

            foreach(int pokemonId in pokemonIds)
            {
                var new_product = CreatePokeProduct(
                    new PokeProduct()
                    {
                        Name = _poke_repo.GetPokemonById(pokemonId).Name,
                        Price = 0,
                        Quantity = 0,
                        InBundle = true,
                        PokemonId = pokemonId
                    });
                
                CreateBundleProduct(
                    new PokeBundleProduct()
                    {
                        PokeBundleId = new_bundle.Id,
                        PokeProductId = new_product.Id
                    }
                );
            }

            return new_bundle;
        }

        public PokeBundle EditPokeBundle(int id, PokeBundle pokeBundle, IEnumerable<int> pokemonIds)
        {
            var edit_bundle = GetPokeBundle(id);
            edit_bundle.Name = pokeBundle.Name;
            edit_bundle.PackDescription = pokeBundle.PackDescription;
            edit_bundle.Price = pokeBundle.Price;
            edit_bundle.Quantity = pokeBundle.Quantity;
            
            _context.SaveChanges();
            
            var pokeBundleProducts = GetPokeBundleProduct(id);
            var pokeProducts = GetPokeProducts().Where(pp => pokeBundleProducts.Select(pbp => pbp.PokeProductId).Contains(pp.Id));
            
            _context.PokeProducts.RemoveRange(pokeProducts);
            _context.PokeBundleProducts.RemoveRange(pokeBundleProducts);
            
            _context.SaveChanges();
            
            foreach (int pokemonId in pokemonIds)
            {
                var new_product = CreatePokeProduct(
                    new PokeProduct()
                    {
                        Name = _poke_repo.GetPokemonById(pokemonId).Name,
                        Price = 0,
                        Quantity = 0,
                        InBundle = true,
                        PokemonId = pokemonId
                    });
                
                CreateBundleProduct(
                    new PokeBundleProduct()
                    {
                        PokeBundleId = id,
                        PokeProductId = new_product.Id
                    }
                );
            }

            return edit_bundle;
        }

        public PokeBundle RemovePokeBundle(int id)
        {
            var remove_bundle = GetPokeBundle(id);
            
            var pokeBundleProducts = GetPokeBundleProduct(id);
            foreach (var item in pokeBundleProducts)
            {
                Console.WriteLine(item.Id + " " + item.PokeBundleId + " " + item.PokeProductId);
            }

            var pokeProducts = GetPokeProducts().Where(pp => pokeBundleProducts.Select(pbp => pbp.PokeProductId).Contains(pp.Id));
            foreach (var item in pokeProducts)
            {
                Console.WriteLine(item.Name + " " + item.PokemonId + " " + item.Id);
            }

            _context.PokeBundleProducts.RemoveRange(pokeBundleProducts.ToList());
            _context.PokeProducts.RemoveRange(pokeProducts.ToList());
            _context.PokeBundles.Remove(remove_bundle);

            _context.SaveChanges();

            return remove_bundle;
        }

        public PokeInvoice CreatePokeInvoice(PokeInvoice pokeInvoice)
        {
            var new_pokeInvoice = _context.PokeInvoices.Add(pokeInvoice);
            _context.SaveChanges();

            return new_pokeInvoice.Entity;
        }

        public OrderItem CreateOrderItem(OrderItem orderItem)
        {
            var new_orderItem = _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            return new_orderItem.Entity;
        }

        public IEnumerable<PokeInvoice> GetPokeInvoices()
        {
            var pokeInvoices = _context.PokeInvoices.ToList();

            return pokeInvoices;
        }

        public PokeInvoice GetPokeInvoice(int id)
        {
            var pokeInvoice = _context.PokeInvoices.Where(pi => pi.Id == id).FirstOrDefault();
            
            return pokeInvoice;
        }

        public IEnumerable<OrderItem> GetOrderItems(int id)
        {
            var orderItems = _context.OrderItems.Where(oi => oi.OrderId == id);

            return orderItems.ToList();
        }
    }
}