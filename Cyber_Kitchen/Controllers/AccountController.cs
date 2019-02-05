using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Security;
using Galactic.ActiveDirectory;
using System.Web.Security;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Collections.Generic;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Interface.Utils;
using System.IO;
using System.Data.Entity;
using Cyber_Kitchen.Infrastructure.Services;

namespace Cyber_Kitchen.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IVoterManager _votMgr;
        private ElasticEmailService _ElasticEmailService;
        private ApplicationUser appUsr;
        //private IExcelProcessor _excel;
        // the bolow roleManager was added
        private RoleManager<IdentityRole> _roleMgr;

        public AccountController(IVoterManager votMgr)
        {
            _votMgr = votMgr;
    

            //_roleMgr = roleMgr;
            // _excel = excel;
        }

        IAuthenticationManager _auth => HttpContext.GetOwinContext().Authentication;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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


        private ApplicationRoleManager roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this.roleManager = value; }
        }

        // THIS COMMENTED PART IS THE CODES TO AUTHENTICATE TO AD

        //GET: /Account/Login

        //[AllowAnonymous]
        // public ActionResult Login(string returnUrl)
        // {
        //     ViewBag.ReturnUrl = returnUrl;
        //     return View();
        // }


        //POST: /Account/Login

        // [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        // public ActionResult Login(LoginModel model, string returnUrl)
        // {
        //     string serverName = ConfigurationManager.AppSettings["ADServer"];
        //     string userName = ConfigurationManager.AppSettings["ADUserName"];
        //     string password = ConfigurationManager.AppSettings["ADPassword"];
        //     try
        //     {

        //         UserProfile usrProfile = new UserProfile();

        //         PrincipalContext adConnect = new PrincipalContext(ContextType.Domain, serverName, userName, password);
        //         UserPrincipal insUserPrincipal = new UserPrincipal(adConnect);

        //         var result = adConnect.ValidateCredentials(model.UserName, model.Password);
        //         UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(adConnect, userName);

        //         var stringGuid = oUserPrincipal.Sid.ToString();
        //         //var email = oUserPrincipal.EmailAddress;
        //         var firstName = oUserPrincipal.GivenName;
        //         var lastName = oUserPrincipal.Surname;
        //         // var auth = adConnect.ValidateCredentials(model.UserName, model.Password);
        //         if (result)// if is true
        //         {
        //             // FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //             List<Claim> claims = new List<Claim>()
        //         {
        //             new Claim(ClaimTypes.NameIdentifier, model.UserName),
        //             new Claim(ClaimTypes.Role, "User"),
        //             new  Claim(ClaimTypes.PrimarySid, oUserPrincipal.Sid.ToString()),
        //            // new Claim(ClaimTypes.PrimarySid,"Sid" ),
        //             new Claim(ClaimTypes.Name, firstName + " " + lastName )
        //         };
        //             var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
        //             //sign in use
        //             _auth.SignIn(identity);
        //             //return RedirectToLocal(returnUrl);
        //             return RedirectToAction("CreateRating", "Rating");
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("", "Invalid username or password");
        //             return View(model);
        //         }

        //     }
        //     catch (Exception ex)
        //     {
        //         // If we got this far, something failed, redisplay form
        //         ModelState.AddModelError("", "" + ex.Message);
        //         return View(model);
        //     }
        // }
        // //
        // POST: /Account/LogOff

        //    [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}
        //////

        //public ActionResult UserProfile()
        //{
        //    string serverName = ConfigurationManager.AppSettings["ADServer"];
        //    string userName = ConfigurationManager.AppSettings["ADUserName"];
        //    string password = ConfigurationManager.AppSettings["ADPassword"];

        //    UserProfile usrProfile = new UserProfile();
        //    try
        //    {
        //        PrincipalContext adConnect = new PrincipalContext(ContextType.Domain,
        //        serverName,
        //         userName,
        //        password);

        //        UserPrincipal insUserPrincipal = new UserPrincipal(adConnect);
        //        var auth = adConnect.ValidateCredentials(userName, password);

        //        UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(adConnect, userName);

        //        var stringGuid = oUserPrincipal.Sid.ToString();
        //        //var email = oUserPrincipal.EmailAddress;
        //        var firstName = oUserPrincipal.GivenName;
        //        var lastName = oUserPrincipal.Surname;

        //    }
        //    catch (Exception x)
        //    {
        //        // unable to connect AD
        //        ModelState.AddModelError("", x.Message);
        //    }
        //    return View(usrProfile);
        //}

        //    #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

         

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //var open = "open";

            if (ModelState.IsValid)
            {
                //if (model.Password.Trim() == "Password123@")
                //{
                //    return RedirectToAction("ResetPassword", "Account");
                //}
                ////////////

                //var results = _votMgr.GetVoters(model.StaffId);

                //if (results == null)
                //{

                //    TempData["message"] = $"Your{""} details does not exist in the database";
                //    return View();
                //}

                var user1 = UserManager.Users.Where(w => w.StaffId == model.StaffId /*&& model.Password == model.Password*/).FirstOrDefault();
                if (user1 == null) return RedirectToAction("login", "account");

                if (user1.IsPasswordChange == true)
                {

                    //Check to see if the password is correct
                    //var token = UserManager.GeneratePasswordResetToken(userId);
                    //UserManager.ResetPassword(userId, token,)
                    var result = await SignInManager.PasswordSignInAsync(model.StaffId, model.Password, model.RememberMe, shouldLockout: false);

                    switch (result)
                    {

                        case SignInStatus.Success:

                            //the bellow codes was added to authenticate the roles
                            var user = UserManager.FindByName(model.StaffId);//.FindByEmailAsync(model.StaffId);

                            var role = UserManager.GetRoles(user.Id).FirstOrDefault();

                            if (role == "Admin")
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else

                                return RedirectToAction("CreateRating", "Rating");


                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View(model);
                    }
                }
                else
                {
                    //User has not changed password
                    return RedirectToAction("ResetPassword", "Account", model);
                }


                //return RedirectToAction("ResetPassword", "Account");


                //var results = _votMgr.GetVoters(model.StaffId);

                //if (results == null)
                //{

                //    TempData["message"] = $"Your{""} details does not exist in the database";
                //    return View();
                //}

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

             
        }

            return View();
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                //return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            // This [] was added to enable dropdown during Registration when a user is registring for the first time.
            // but the dropdown is hiden for the user. it can only be seen by the dmin.


            var roles = new string[] { "Admin", "User" };
            ViewBag.proinfo = new SelectList(roles);

            //ViewBag.voters = new SelectList(_votMgr.GetVoters().Result, "VoterId", "StaffName");
            return View();
        }

        //note that to be 'Async' means not occuring at the same time while sync occur at the same time

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //var results = _votMgr.GetVoters(model.Email);

            //if (results == null)
            //{
            //    TempData["message"] = $"Your{""} details does not exist in the database";
            //    return View();
            //}

            if (ModelState.IsValid)
            {

                var roles = new string[] { "Admin", "User" };
                ViewBag.proinfo = new SelectList(roles);



                var user = new ApplicationUser { UserName = model.StaffId, StaffId = model.StaffId };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //
                    if (User.IsInRole("Admin"))
                    {
                        await UserManager.AddToRoleAsync(user.Id, model.Role);
                    }




                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //if (User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("Index", "Home");

                    return RedirectToAction("ResetPassword", "Account");
                }
            
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            }
        
    
                

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            //var open = "open";
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["Message"] = "User Not exist.";
                }

                //if (model.Password.Trim().ToLower() == open)
                //{
                //dosomething

                //var user = await UserManager.FindByNameAsync(model.Email);

                //var staff = _votMgr.GetVoters(model.Email);
                //    if(staff == null)
                //{
                //    TempData["Message"] = "User Not exist.";
                //}
                //For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                _ElasticEmailService = new ElasticEmailService();
                await _ElasticEmailService.Send(user.Email, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">Reset</a>");
                   //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                //}


                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}


            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code, LoginViewModel LoginModel)
        {
            ViewBag.staffno = LoginModel.StaffId.ToString();
            // return code == null ? View("Error") : View();
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.StaffId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, token, model.Password);
            if (result.Succeeded && user.IsPasswordChange == false)
            {

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var objUser = context.Users.FirstOrDefault(x => x.StaffId == user.StaffId);
                    objUser.IsPasswordChange = true;
                    context.Entry<ApplicationUser>(objUser).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return RedirectToAction("Login", "Account");
            }
            AddErrors(result);
            return View();
        }

       
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                //return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

    }
}
