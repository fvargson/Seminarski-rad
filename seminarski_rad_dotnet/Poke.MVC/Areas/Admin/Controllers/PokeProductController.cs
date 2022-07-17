using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poke.Data.Interface;
using Poke.Data.Model;

namespace Poke.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PokeProductController : Controller
    {
        private readonly IPokemonRepository _poke_repo;
        private readonly IApplicationRepo _app_repo;

        public PokeProductController(IPokemonRepository poke_repo, IApplicationRepo app_repo)
        {
            _app_repo = app_repo;
            _poke_repo = poke_repo;
        }

        // GET: PokeProduct
        public ActionResult Index(string? msg)
        {
            if(msg != null)
            {
                ViewBag.msg = msg;
            }
            return View(_app_repo.GetPokeProducts().Where(pp => !pp.InBundle).OrderBy(pp => pp.PokemonId));
        }

        // GET: PokeProduct/Details/5
        public ActionResult Details(int id)
        {
            var poke = _app_repo.GetPokeProductById(id);
            
            if(poke == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.myPokemon = _poke_repo.GetPokemonById(poke.PokemonId);
            ViewBag.pokeInfo = _poke_repo.GetPokeInfo(poke.PokemonId);

            return View(poke);
        }

        // GET: PokeProduct/Create
        public ActionResult Create()
        {
            ViewBag.Pokemons = _poke_repo.GetPokemon(898, 0);
            return View();
        }

        // POST: PokeProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PokeProduct pokeProduct)
        {
            try
            {
                pokeProduct.InBundle = false;

                if(pokeProduct.PokemonId <= 0)
                    return RedirectToAction("Index");
                
                var new_product = _app_repo.CreatePokeProduct(pokeProduct);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: PokeProduct/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Pokemons = _poke_repo.GetPokemon(898, 0);

            var poke = _app_repo.GetPokeProductById(id);
            
            return View(poke);
        }

        // POST: PokeProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PokeProduct pokeProduct)
        {
            try
            {
                var poke = _app_repo.GetPokeProductById(id);

                if(poke == null)
                {
                    return RedirectToAction("Index");
                }

                pokeProduct.InBundle = false;
                
                _app_repo.EditPokeProduct(id, pokeProduct);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: PokeProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PokeProduct/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PokeProduct pokeProduct)
        {
            try
            {
                var poke = _app_repo.GetPokeProductById(id);

                if(poke == null)
                {
                    return RedirectToAction("Index");
                }

                _app_repo.RemovePokeProduct(pokeProduct.Id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }
    }
}