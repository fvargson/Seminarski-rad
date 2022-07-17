using Microsoft.AspNetCore.Mvc;
using Poke.MVC.Extensions;
using Poke.Data.Interface;
using Poke.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Poke.MVC.Controllers
{
    public class CartController : Controller
    {
        public const string SessionKeyName = "_cart";
        private readonly IApplicationRepo _app_repo;
        private readonly UserManager<IdentityUser> _user_manager;
        public CartController(IApplicationRepo app_repo, UserManager<IdentityUser> user_manager)
        {
            _app_repo = app_repo;
            _user_manager = user_manager;
        }

        // GET: Cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();

            ViewBag.Total = cart.Sum(p => p.GetTotal());

            return View(cart);
        }

        // POST Cart/Create
        [HttpPost]
        public IActionResult AddProductToCart(int id, int quantity)
        {
            if(quantity == 0)
            {
                return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add 0 amount." });
            }
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            
            var find_product = _app_repo.GetPokeProductById(id);
            
            if(find_product == null)
            {
                return RedirectToAction("Product", "Home", new { msg = "There is no more of this product available" });
            }

            if(cart.Count == 0)
            {
                if(quantity > find_product.Quantity)
                {
                    return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add product to Cart." });
                }
                CartItem new_item = new CartItem()
                {
                    Product = find_product,
                    Quantity = quantity
                };

                cart.Add(new_item);
                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            else
            {
                var update_product = cart.Where(ci => ci.Product != null).ToList().Find(ci => ci.Product.Id == id) ?? new CartItem();

                if(quantity + update_product.Quantity > find_product.Quantity)
                {
                    return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add that many products." });
                }
                if(update_product.Quantity == 0)
                {
                    update_product.Product = find_product;
                    update_product.Quantity = quantity;
                    cart.Add(update_product);
                }
                else
                {
                    update_product.Quantity += quantity;
                }

                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddBundleToCart(int id, int quantity)
        {
            if(quantity == 0)
            {
                return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add 0 amount." });
            }
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            
            var find_bundle = _app_repo.GetPokeBundle(id);
            
            if(find_bundle == null)
            {
                return RedirectToAction("Product", "Home", new { msg = "There is no more of this Bundle available" });
            }

            if(cart.Count == 0)
            {
                if(quantity > find_bundle.Quantity)
                {
                    return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add Bundle to Cart." });
                }
                CartItem new_item = new CartItem()
                {
                    Bundle = find_bundle,
                    Quantity = quantity
                };

                cart.Add(new_item);
                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            else
            {
                var update_bundle = cart.Where(ci => ci.Bundle != null).ToList().Find(ci => ci.Bundle.Id == id) ?? new CartItem();

                if(quantity + update_bundle.Quantity > find_bundle.Quantity)
                {
                    return RedirectToAction("Product", "Home", new { id = id, msg = "Cannot add that many Bundles." });
                }
                if(update_bundle.Quantity == 0)
                {
                    update_bundle.Bundle = find_bundle;
                    update_bundle.Quantity = quantity;
                    cart.Add(update_bundle);
                }
                else
                {
                    update_bundle.Quantity += quantity;
                }

                HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveProductFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            
            cart.RemoveAll(p => p.Product.Id == id);
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveBundleFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();
            
            cart.RemoveAll(p => p.Bundle.Id == id);
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();

            cart.RemoveAll(p => p.Quantity > 0);

            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Order(List<string>? errors)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();

            ViewBag.TotalPrice = cart.Sum(ci => ci.GetTotal());
            ViewBag.errors = errors;

            return View(cart);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(PokeInvoice pokeInvoice)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName) ?? new List<CartItem>();

            if(cart.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var model_errors = new List<string>();

            if(!ModelState.IsValid) 
            {
                foreach(var modelState in ModelState.Values)
                {
                    foreach(var find_error in modelState.Errors)
                    {
                        model_errors.Add(find_error.ErrorMessage);
                    }
                }
                return RedirectToAction("Order", new { errors = model_errors } );
            }

            Console.WriteLine(pokeInvoice.LastName);

            var userId = _user_manager.GetUserId(User);

            pokeInvoice.DateCreated = DateTime.Now;
            pokeInvoice.UserId = userId;

            var new_invoice = _app_repo.CreatePokeInvoice(pokeInvoice);

            foreach(var item in cart)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = new_invoice.Id,
                    Quantity = item.Quantity,
                    Total = item.GetTotal()
                };
                if(item.Product != null)
                {
                    orderItem.ProductTitle = item.Product.Name;
                    orderItem.Price = item.Product.Price;
                    orderItem.ProductId = item.Product.Id;
                }
                else
                {
                    orderItem.ProductTitle = item.Bundle.Name;
                    orderItem.Price = item.Bundle.Price;
                    orderItem.BundleId = item.Bundle.Id;
                    orderItem.ProductDescription = item.Bundle.PackDescription;
                }
                _app_repo.CreateOrderItem(orderItem);
            }

            cart.RemoveAll(ci => ci.Quantity > 0);

            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            return RedirectToAction("Invoice", "Cart", new { Id = new_invoice.Id });
        }

        public IActionResult Invoice(int Id)
        {
            var invoice = _app_repo.GetPokeInvoice(Id);
            var items = _app_repo.GetOrderItems(Id);

            ViewBag.Total = items.Sum(oi => oi.Total);
            ViewBag.Items = items.ToList();

            return View(invoice);
        }
    }
}