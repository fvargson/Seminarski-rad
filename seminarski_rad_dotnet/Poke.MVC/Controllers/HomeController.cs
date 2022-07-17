using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Poke.Data.Interface;
using Poke.Data.Model;
using Poke.MVC.Models;

namespace Poke.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPokemonRepository _poke_repo;
    private readonly IApplicationRepo _app_repo;

    public HomeController(ILogger<HomeController> logger, IPokemonRepository poke_repo, IApplicationRepo app_repo)
    {
        _logger = logger;
        _poke_repo = poke_repo;
        _app_repo = app_repo;
    }

    public IActionResult Index(int id)
    {
        var all_bundles = _app_repo.GetPokeBundles().ToList();
        ViewBag.categories = _app_repo.GetPokeCategories().ToList();
        ViewBag.bundles = all_bundles;
        ViewBag.subCategory_id = id;
        ViewBag.subCategories = _app_repo.GetSubCategories(1).Union(_app_repo.GetSubCategories(2)).Union(_app_repo.GetSubCategories(3)).ToList();

        if(id == 0)
        {
            var all_products = _app_repo.GetPokeProducts().Where(pp => !pp.InBundle).ToList();
        
            if(all_products.Count <= 10)
            {
                return View(all_products);
            }
            
            Random rand = new Random();
            List<int> possible = Enumerable.Range(0, all_products.Count).ToList();
            List<int> listNumbers = new List<int>();
            
            for (int i = 0; i < 10; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            var rand_products = new List<Poke.Data.Model.PokeProduct>();
            
            foreach (var item in listNumbers)
            {
                rand_products.Add(all_products[item]);
            }
            return View(rand_products.OrderBy(pp => pp.PokemonId).ToList());
        }
        else
        {
            var subCategory = _app_repo.GetSubCategory(id);
            
            var pokeProductCategory = _app_repo.GetPokeProductCategories().Where(ppc => ppc.SubCategoryId == id).Select(ppc => ppc.PokeProductId);

            var all_products_in_category = _app_repo.GetPokeProducts().Where(pp => pokeProductCategory.Contains(pp.Id));

            var bundle_products = all_products_in_category.Where(pp => pp.InBundle);
            
            var pokeBundleProducts_in_category = _app_repo.GetPokeBundleProducts().Where(pbp => bundle_products.Select(pbp => pbp.Id).Contains(pbp.PokeProductId));

            var bundles = all_bundles.Where(pb => pokeBundleProducts_in_category.Select(pbp => pbp.PokeBundleId).Contains(pb.Id)).Distinct().ToList();

            ViewBag.bundles = bundles;

            return View(all_products_in_category.Where(pp => !pp.InBundle).OrderBy(pp => pp.PokemonId).ToList());
        }
    }

    public IActionResult Product(int id)
    {
        var product = _app_repo.GetPokeProductById(id);

        if(product == null)
        {
            return RedirectToAction("Index");
        }

        var pokemon = _poke_repo.GetPokemonById(product.PokemonId);
        var sc_info = _poke_repo.GetPokeInfo(product.PokemonId);

        ViewBag.pokemon = pokemon;
        ViewBag.sc_info = sc_info;

        return View(product);
    }

    public IActionResult Bundle(int id)
    {
        var bundle = _app_repo.GetPokeBundle(id);

        if(bundle == null)
        {
            return RedirectToAction("Index");
        }

        var pokemon = new List<MyPokemon>();
        var pokeProductsIds = _app_repo.GetPokeBundleProduct(bundle.Id).Select(pbp => pbp.PokeProductId).Distinct().ToList();
        var pokeProducts = _app_repo.GetPokeProducts().Where(pp => pokeProductsIds.Contains(pp.Id)).Select(pp => pp.PokemonId).ToList();
        pokeProducts.ForEach(p => pokemon.Add(_poke_repo.GetPokemonById(p)));
        
        var sc_infos = new List<PokeApiNet.PokemonSpecies>();

        pokeProducts.ForEach(p => sc_infos.Add(_poke_repo.GetPokeInfo(p)));

        ViewBag.pokemons = pokemon;
        ViewBag.sc_infos = sc_infos;

        return View(bundle);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
