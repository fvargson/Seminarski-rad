using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Poke.Data.Interface;

namespace Poke.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _user_manager;
        private readonly RoleManager<IdentityRole> _role_manager;
        private readonly IApplicationRepo _app_repo;
        public UsersController(UserManager<IdentityUser> user_manager, RoleManager<IdentityRole> role_manager, IApplicationRepo app_repo)
        {
            _user_manager = user_manager;
            _role_manager = role_manager;
            _app_repo = app_repo;
        }
        // GET: Users
        public IActionResult Index(string? msg)
        {
            if(msg != null)
            {
                ViewBag.msg = msg;
            }
            var users = _user_manager.Users.ToList();

            return View(users);
        }

        // GET: Users/Details/5
        public IActionResult Details(string id)
        {
            var user = _user_manager.Users.SingleOrDefault(u => u.Id == id);

            var invoices = _app_repo.GetPokeInvoices().Where(pi => pi.UserId == id).ToList();
            
            var totals = new List<(int, int)>();

            foreach(var item in invoices)
            {
                totals.Add((_app_repo.GetOrderItems(item.Id).Sum(oi => oi.Total), item.Id));
            }

            ViewBag.Totals = totals;
            ViewBag.invoices = invoices;

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create(List<string>? errors = null)
        {
            if(errors != null)
            {
                ViewBag.errors = errors;
            }

            var roles = _role_manager.Roles.ToList();

            ViewBag.roles = roles;

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Role, string email, string password, string Confirm)
        {
            try
            {
                if(Confirm != password)
                {
                    return RedirectToAction("Create", new { msg = "Match passowrds." });
                }

                var identityUser = new IdentityUser()
                    {
                        Email = email,
                        UserName = email,
                        NormalizedEmail = email.ToUpper(),
                        NormalizedUserName = email.ToUpper(),
                        PhoneNumberConfirmed = false,
                        EmailConfirmed = false
                    };

                var result = _user_manager.CreateAsync(identityUser, password).Result;

                if(_role_manager.RoleExistsAsync(Role).Result)
                {
                    _user_manager.AddToRoleAsync(identityUser, Role);
                }

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create", new { errors = result.Errors.Select(error => error.Description).ToList() });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { error = "[Dev]Some error: " + ex.Message } );
            }
        }

        // GET: Users/Edit/5
        public IActionResult Edit(string id, List<string>? errors = null)
        {
            if(errors != null)
            {
                ViewBag.errors = errors;
            }

            var user = _user_manager.Users.SingleOrDefault(u => u.Id == id);

            var roles = _role_manager.Roles.ToList();

            ViewBag.roles = roles;

            if(_user_manager.IsInRoleAsync(user, "Admin").Result)
            {
                ViewBag.Role = "Admin";
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Id, string email, string phoneNumber, bool EmailConfirmed, string? password, string? adminPass, string? Role)
        {
            try
            {
                var user = _user_manager.Users.SingleOrDefault(u => u.Id == Id);

                if(user == null)
                {
                    return RedirectToAction("Index");
                }

                var admin = _user_manager.GetUserAsync(User).Result;

                if(password != null)
                {
                    var checkAdminPass = _user_manager.CheckPasswordAsync(admin, adminPass).Result;
                    if(checkAdminPass)
                    {
                        var token = _user_manager.GeneratePasswordResetTokenAsync(user).Result;

                        _user_manager.ResetPasswordAsync(user, token, password);
                    }
                    else
                    {
                        return RedirectToAction();
                    }
                }

                if(Role != null)
                {
                    if(Role == "Admin")
                    {
                        _user_manager.AddToRoleAsync(user, "Admin");
                    }
                }

                user.Email = email;
                user.PhoneNumber = phoneNumber;
                user.EmailConfirmed = EmailConfirmed;
                user.UserName = email;
                user.NormalizedEmail = email.ToUpper();
                user.NormalizedUserName = email.ToUpper();
                
                var result = _user_manager.UpdateAsync(user).Result;

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Edit", new { id = Id, errors = result.Errors.Select(error => error.Description).ToList() });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { msg = "[Dev]Some error " + ex.Message });
            }
        }

        // GET: Users/Delete/5
        public IActionResult Delete(string id, List<string>? errors)
        {
            if(errors != null)
            {
                ViewBag.errors = errors;
            }

            var user = _user_manager.Users.SingleOrDefault(u => u.Id == id);

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var user = _user_manager.Users.SingleOrDefault(u => u.Id == id);

                if(user == null)
                {
                    return RedirectToAction("Index");
                }

                var result = _user_manager.DeleteAsync(user).Result;

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Delete", new { id = id, errors = result.Errors.Select(error => error.Description).ToList() });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}