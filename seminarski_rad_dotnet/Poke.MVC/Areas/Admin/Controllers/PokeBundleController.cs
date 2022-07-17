using Poke.Data.Model;
using Poke.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Poke.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PokeBundleController : Controller
    {
        private readonly IApplicationRepo _app_repo;
        private readonly IPokemonRepository _poke_repo;
        public PokeBundleController(IApplicationRepo app_repo, IPokemonRepository poke_repo)
        {
            _app_repo = app_repo;
            _poke_repo = poke_repo;
        }
        // GET: PokeBundle
        public ActionResult Index()
        {
            return View(_app_repo.GetPokeBundles());
        }

        // GET: PokeBundle/Details/5
        public ActionResult Details(int id)
        {
            var pokeBundle = _app_repo.GetPokeBundle(id);
            var pokeBundleProducts = _app_repo.GetPokeBundleProducts();
            var products = _app_repo.GetPokeProducts().Where(pp => pokeBundleProducts.Where(pbp => pbp.PokeBundleId == id).Select(pbp => pbp.PokeProductId).Contains(pp.Id)).ToList();
            
            ViewBag.products = products;
            return View(pokeBundle);
        }

        // GET: PokeBundle/Create
        public ActionResult Create()
        {
            var pokes = _poke_repo.GetPokemon(898, 0);
            ViewBag.pokes = pokes;
            return View();
        }

        // POST: PokeBundle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokeBundle pokeBundle, IEnumerable<int> PokemonIds)
        {
            try
            {
                if(PokemonIds.Count() < 2)
                {
                    return RedirectToAction("Create", new { msg = "Cannot make a pack, add at least 2 Pokemon to the pack." });
                }

                _app_repo.CreatePokeBundle(pokeBundle, PokemonIds);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: PokeBundle/Edit/5
        public ActionResult Edit(int id)
        {
            var pokes = _poke_repo.GetPokemon(898, 0);
            ViewBag.pokes = pokes;

            var bundle = _app_repo.GetPokeBundle(id);

            var bundleProduct = _app_repo.GetPokeBundleProduct(id).Select(pbp => pbp.PokeProductId);
            var pokeProducts = _app_repo.GetPokeProducts().Where(pp => bundleProduct.Contains(pp.Id)).Select(pp => pp.PokemonId);
            var added_pokemon = pokes.Where(mp => pokeProducts.Contains(mp.Id)).ToList();
            ViewBag.added_pokemon = added_pokemon;
            return View(bundle);
        }

        // POST: PokeBundle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PokeBundle pokeBundle, IEnumerable<int> PokemonIds)
        {
            try
            {
                if(PokemonIds.Count() < 2)
                {
                    return RedirectToAction("Edit", new { id = id, msg = "Cannot make a pack, add at least 2 Pokemon to the pack." });
                }

                _app_repo.EditPokeBundle(id, pokeBundle, PokemonIds);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: PokeBundle/Delete/5
        public ActionResult Delete(int id)
        {
            var pokes = _poke_repo.GetPokemon(898, 0);

            var bundle = _app_repo.GetPokeBundle(id);

            var bundleProduct = _app_repo.GetPokeBundleProduct(id).Select(pbp => pbp.PokeProductId);
            var pokeProducts = _app_repo.GetPokeProducts().Where(pp => bundleProduct.Contains(pp.Id)).Select(pp => pp.PokemonId);
            var added_pokemon = pokes.Where(mp => pokeProducts.Contains(mp.Id)).ToList();

            ViewBag.added_pokemon = added_pokemon;

            return View(bundle);
        }

        // POST: PokeBundle/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PokeBundle pokeBundle)
        {
            try
            {
                _app_repo.RemovePokeBundle(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }
    }
}