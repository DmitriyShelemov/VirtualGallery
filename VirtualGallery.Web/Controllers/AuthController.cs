using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Auth;

namespace VirtualGallery.Web.Controllers
{
    public partial class AuthController : BaseController
    {
        public AuthController(IWorkContext workContext)
            : base(workContext)
        {
        }

		// GET: /Auth/Login
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl, string username, int? error)
        {
            var loginModel = new LoginModel
	            {
		            ReturnUrl = returnUrl,
					UserName = username,
					Expired = error.HasValue && error.Value == Convert.ToInt32(HttpStatusCode.Unauthorized)
	            };
            if (error.HasValue && error.Value == (int)HttpStatusCode.Unauthorized)
            {
				ModelState.AddModelError(string.Empty, Localization.Security_Session_has_expired);
				ModelState.AddModelError(string.Empty, Localization.Common_Please_Login_Again);
            }
            return View(loginModel);
        }

		// POST: /Auth/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginModel model)
        {
            using (var cryptoProvider = new MD5CryptoServiceProvider())
            {
                if (ModelState.IsValid
                    && model.UserName == AppSettings.AdminName
                    && Convert.ToBase64String(cryptoProvider.ComputeHash(Encoding.Unicode.GetBytes(model.Password))) == AppSettings.AdminPwd)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    WorkContext.LoginAs(new UserInfo { UserName = AppSettings.AdminName });
                    return RedirectToLocal(model.ReturnUrl);
                }
            }
            
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, Localization.Account_Incorrect_username_or_password);
            return View(model);
        }

        // GET: /Auth/Logout
        [AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            WorkContext.Logout();
            return RedirectToAction(MVC.Auth.Login());
        }

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction(MVC.Home.Index());
		}
    }
}
