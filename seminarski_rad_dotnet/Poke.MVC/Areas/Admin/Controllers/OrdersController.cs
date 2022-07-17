using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poke.Data.Interface;
using Microsoft.AspNetCore.Identity;

namespace Poke.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IApplicationRepo _app_repo;
        private readonly UserManager<IdentityUser> _user_manager;
        public OrdersController(IApplicationRepo app_repo, UserManager<IdentityUser> user_manager)
        {
            _app_repo = app_repo;
            _user_manager = user_manager;
        }
        // GET: Orders
        public ActionResult Index()
        {
            var orders = _app_repo.GetPokeInvoices();

            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            var order = _app_repo.GetPokeInvoice(id);

            if(order == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Invoice", "Cart" , new { area = "", Id = id });
        }
    }
}