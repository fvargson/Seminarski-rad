using Microsoft.AspNetCore.Authorization;
using Poke.Data.Model;
using Poke.Data.Repository;
using Poke.Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Poke.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly IApplicationRepo _app_repo;
        public SubCategoryController(IApplicationRepo app_repo)
        {
            _app_repo = app_repo;
        }
        // GET: SubCategory
        public ActionResult Index()
        {
            List<SubCategory> subCategories = new List<SubCategory>();

            ViewBag.categories = _app_repo.GetPokeCategories();

            for(int i = 1; i < 4; i++)
            {
                subCategories.AddRange(_app_repo.GetSubCategories(i));
            }
            
            return View(subCategories);
        }

        // GET: SubCategory/Details/5
        public ActionResult Details(int id)
        {
            var subCategory = _app_repo.GetSubCategory(id);
            var pokeProductCategories = _app_repo.GetPokeProductCategories();
            var productsOfSc = _app_repo
                                    .GetPokeProducts()
                                    .Where(
                                        pp => pokeProductCategories
                                                    .Where(
                                                        ppc => ppc.PokeProductId == pp.Id
                                                    ).Select(
                                                        ppc => ppc.SubCategoryId
                                                    ).Contains(subCategory.Id)
                                    );
            IPokemonRepository _poke_repo = new PokeRepository();
            ViewBag.products = _poke_repo.GetPokemon(898, 0).Where(poke => productsOfSc.Where(p => !p.InBundle).Select(p => p.PokemonId).Contains(poke.Id)).ToList();
            
            var productsInBundle = _app_repo.GetPokeProducts().Where(pp => pp.InBundle).Select(pp => pp.Id);
            var bundles = _app_repo.GetPokeBundles();
            var bundleProduct = _app_repo.GetPokeBundleProducts().Where(pbp => productsInBundle.Contains(pbp.PokeProductId)).Select(pbp => pbp.PokeBundleId);
            
            ViewBag.bundles = bundles.Where(pb => bundleProduct.Contains(pb.Id)).ToList();
            
            ViewBag.category = _app_repo.GetPokeCategories().FirstOrDefault(c => c.Id == subCategory.PokeCategoryId).Name;
            
            return View(subCategory);
        }

        // GET: SubCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory new_subcategory)
        {
            try
            {
                new_subcategory.PokeCategoryId = 3;
                _app_repo.CreateSubCategory(new_subcategory);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: SubCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_app_repo.GetSubCategory(id));
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubCategory subCategory)
        {
            try
            {
                var subC = _app_repo.GetSubCategory(id);
                if(subC == null)
                {
                    return RedirectToAction("Index");
                }

                _app_repo.EditSubCategory(id, subCategory);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }

        // GET: SubCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_app_repo.GetSubCategory(id));
        }

        // POST: SubCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SubCategory subCategory)
        {
            try
            {
                var productSubCategories = _app_repo.GetPokeProductCategories();
                var products = _app_repo.GetPokeProducts()
                                    .Where(
                                        pp => productSubCategories
                                                    .Where(
                                                        ppc => ppc.SubCategoryId == id
                                                    ).Select(
                                                        ppc => ppc.PokeProductId == pp.Id
                                                    ).FirstOrDefault()
                                    );
                if(products != null)
                {
                    if(products.Count() > 0)
                    {
                        return RedirectToAction("Index", new { msg = "Cannot delete Category that has Products that are tied to this Category." });
                    }
                }
                _app_repo.RemoveSubCategory(id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some fail: " + ex.Message });
            }
        }
    }
}