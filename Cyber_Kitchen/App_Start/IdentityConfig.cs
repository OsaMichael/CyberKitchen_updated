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
using Cyber_Kitchen.Models;
using System.Net.Mail;
using System.Net;
using EASendMail;

namespace Cyber_Kitchen
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            // Credentials:

            //var sClient = new System.Net.Mail.SmtpClient("www.cyberspace.net.ng");
            //var mailmessage = new MailMessage();

            //sClient.Port = 25;
            //sClient.EnableSsl = false;
            //sClient.Credentials = new NetworkCredential("noreply@cyberpsace.net.ng", "Biling123");
            //sClient.UseDefaultCredentials = false;

            //mailmessage.Body = "Test";
            //mailmessage.From = new System.Net.Mail.MailAddress("noreply@cyberpsace.net.ng");
            //mailmessage.Subject = "Test";
            //mailmessage.CC.Add(new System.Net.Mail.MailAddress("michael.aruebo@cyberspace.net.ng"));

            //sClient.Send(mailmessage);

            //try
            //{
            //    Console.WriteLine("start to send email to queue...");
            //    sClient.SendMailAsync(mailmessage);
            //    Console.WriteLine("email was sent successfully!");
            //}
            //catch (Exception ep)
            //{
            //    Console.WriteLine("failed to send email with the following error:");
            //    Console.WriteLine(ep.Message);
            //}

            //SmtpMail oMail = new SmtpMail("TryIt");
            //EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            //// Set sender email address, please change it to yours
            //oMail.From = "michael.aruebo@cyberspace.net.ng";

            //// Set recipient email address, please change it to yours
            //oMail.To = "babakenny@gmail.com";

            //// Set email subject
            //oMail.Subject = "test email from c# database queue";

            //// Set email body
            //oMail.TextBody = "this is a test email sent from c# project, do not reply";

            //// Do not set SMTP server address
            //SmtpServer oServer = new SmtpServer("www.cyberspace.net.ng");

            //try
            //{
            //    Console.WriteLine("start to send email to queue...");
            //    oSmtp.SendMail(oServer, oMail);
            //    Console.WriteLine("email was sent successfully!");
            //}
            //catch (Exception ep)
            //{
            //    Console.WriteLine("failed to send email with the following error:");
            //    Console.WriteLine(ep.Message);
            //}

            //now construct a MailMessage object from the message
            //var mailMessage = new MailMessage
            //("michael.aruebo@cyberspace.net.ng", message.Destination, message.Subject, message.Body);

            //mailMessage.IsBodyHtml = true;

            ////create a client, it should pick up the settings from web.config
            //using (var client = new System.Net.Mail.SmtpClient())
            //{
            //    // //send the email asynchronously
            //    await client.SendMailAsync(mailMessage);
            //}

            //return Task.FromResult(0);
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

            //// Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = true,
            //    RequireLowercase = false,
            //    RequireUppercase = true,
            //};

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


    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
            return new ApplicationRoleManager(roleStore);
        }
    }
}
