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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookService(BookServiceViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }
            if (UserManager.EmailService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }

           // ModelState.AddModelError("", "Invalid login attempt.");
            ViewBag.returnmessage = "Thanks for your interest. We will get back to you shortly.";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookServiceAjax(BookServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                string errorlist = "";
                foreach (ModelError error in allErrors)
                {
                     errorlist = error.ErrorMessage;
                }
                return Json(new { error = errorlist });
            }
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }
            if (UserManager.EmailService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return Json(new { success = true });
        }


         [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContactUs(BookServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                string errorlist = "";
                foreach (ModelError error in allErrors)
                {
                     errorlist = error.ErrorMessage;
                }
                return Json(new { error = errorlist });
            }
            if (UserManager.EmailService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = "+919819355066",
                    Body = model.Number + " This booked a service"
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return Json(new { success = true });
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