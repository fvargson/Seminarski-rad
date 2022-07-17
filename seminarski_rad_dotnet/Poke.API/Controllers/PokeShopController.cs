using Microsoft.AspNetCore.Mvc;
using Poke.Data.Interface;
using Poke.Data.Model;

namespace Poke.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeShopController : ControllerBase
    {
        private readonly IApplicationRepo _app_repo;
        public PokeShopController(IApplicationRepo app_repo)
        {
            _app_repo = app_repo;
        }

        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<PokeProduct>> GetPokeProducts()
        {
            try
            {
                return Ok(_app_repo.GetPokeProducts().Where(pp => !pp.InBundle));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an Error");
            }
        }

        [HttpGet]
        [Route("bundles")]
        public ActionResult<IEnumerable<PokeBundle>> GetPokeBundles()
        {
            try
            {
                return Ok(_app_repo.GetPokeBundles());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error");
            }
        }

        [HttpGet]
        [Route("product/{id:int}")]
        public ActionResult<PokeProduct> GetPokeProduct(int id)
        {
            try
            {
                var find_product = _app_repo.GetPokeProductById(id);

                if(find_product == null || find_product.InBundle)
                {
                    return BadRequest("No product found");
                }

                return Ok(find_product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error");
            }
        }

        [HttpGet]
        [Route("bundle/{id:int}")]
        public ActionResult<object> GetPokeBundle(int id)
        {
            try
            {
                var find_bundle = _app_repo.GetPokeBundle(id);

                if(find_bundle == null)
                {
                    return BadRequest("No product found");
                }

                var pokeProductBundle = _app_repo.GetPokeBundleProduct(id);
                var pokeProducts = _app_repo.GetPokeProducts().Where(pp => pokeProductBundle.Select(pbp => pbp.PokeProductId).Contains(pp.Id));

                return Ok(
                    new {
                        Bundle = find_bundle,
                        Products = pokeProducts
                    });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error");
            }
        }
    }
}
