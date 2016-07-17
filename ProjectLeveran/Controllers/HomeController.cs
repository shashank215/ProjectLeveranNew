using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectLeveran.Models;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace ProjectLeveran.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "We run your errands while you do more important things in life.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get In Touch.";

            return View();
        }
        public ActionResult Services()
        {
            ViewBag.Message = "Services We Offer";

            return View();
        }
        public ActionResult BookService()
        {
            ViewBag.Message = "Book A Service";

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       // public async Task<ActionResult> Book(BookServiceViewModel model, string returnUrl)
         public  ActionResult Book(BookServiceViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
             return RedirectToLocal(returnUrl);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result=SignInStatus.Success//=await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }

        [HttpPost]
          [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookService(BookServiceViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //return RedirectToLocal(returnUrl);
            
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number +" This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }
            if (UserManager.EmailService  != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.EmailService.SendAsync(message);
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
                   // return RedirectToLocal(returnUrl);
          //  return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}