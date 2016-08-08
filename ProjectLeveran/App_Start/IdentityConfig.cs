using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ProjectLeveran.Models;
using System.Net.Mail;
using System.Configuration;

namespace ProjectLeveran
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string host = ConfigurationManager.AppSettings["mailhost"];
            string port = ConfigurationManager.AppSettings["mailport"];
            string fromId = ConfigurationManager.AppSettings["fromId"]; ;
            string pwd = ConfigurationManager.AppSettings["pwd"];
            string mailTo = ConfigurationManager.AppSettings["mailTo"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port))
            {
                return Task.FromResult(1);
            }
            

            // Instantiate a new instance of MailMessage
            try
            {
                using (MailMessage mMailMessage = new MailMessage())
                {
                    mMailMessage.From = new MailAddress(fromId);
                    mMailMessage.To.Add(new MailAddress(mailTo));


                    mMailMessage.Subject = message.Subject;
                    mMailMessage.Body = message.Body;
                  // mMailMessage.Priority = MailPriority.Normal;
                    mMailMessage.IsBodyHtml = true;
                    SmtpClient mSmtpClient = new SmtpClient();
                    mSmtpClient.UseDefaultCredentials = false; 
                   // mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network ;
                    mSmtpClient.Host = host;
                    mSmtpClient.Port = Int32.Parse(port);
                    mSmtpClient.Credentials = new System.Net.NetworkCredential(fromId, pwd);
                    mSmtpClient.Send(mMailMessage);
                    return Task.FromResult(0);
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                return Task.FromResult(1);
            }

            //return Task.FromResult(1);

            // Plug in your email service here to send an email.
           // return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
