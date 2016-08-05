using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmptyAsosTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            SignInManager.AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Account");
        }

        private IdentityHubSignInManager _signInManager;
        public IdentityHubSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<IdentityHubSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private IdentityHubUserManager _userManager;
        public IdentityHubUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<IdentityHubUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}