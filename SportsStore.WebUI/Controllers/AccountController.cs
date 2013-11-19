using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.Linq;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private  IAuthProvider _authProvider;

        public AccountController(IAuthProvider prov)
        {
            _authProvider = prov;
        }

        [AllowAnonymous]
        public ViewResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnViewModel model, String returnUrl)
        {
            if (ModelState.IsValid) {
                if (_authProvider.Authenticate(model.UserName, model.Password)) {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                } else {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            } else {
                return View();
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = _authProvider.RegisterUser(user, model.Password);
                if (result != null && result.Count() == 0) {
                    return Redirect("/");
                }
                foreach (var error in result) {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
    }
}
