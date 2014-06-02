using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.WorkContext;

namespace VirtualGallery.Web.Infrastructure.Filters
{
    public class GalleryAuthorizeAttribute : AuthorizeAttribute
    {
        private static string _sessionCookieName;

		private IWorkContext GetWorkContext()
		{
			return DependencyResolver.Current.GetService<IWorkContext>();
		}
        
        private static string SessionCookieName
        {
            get
            {
                if (string.IsNullOrEmpty(_sessionCookieName))
                {
                    var sessionStateSection =
                        (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
                    _sessionCookieName = sessionStateSection.CookieName;
                }

                return _sessionCookieName;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return IsAuthorized(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();

                throw new AuthorizationException();
            }
            
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["Cookie"])
                     && !string.IsNullOrEmpty(SessionCookieName)
                     && filterContext.HttpContext.Request.Headers["Cookie"].Contains(SessionCookieName))
            {
                var path = filterContext.RouteData.Route.GetVirtualPath(
                    filterContext.RequestContext, filterContext.RouteData.Values);

                var currentUser = GetWorkContext().GetUser();
                var returnUrl = (path != null && !string.IsNullOrEmpty(path.VirtualPath))
                    ? (VirtualPathUtility.Combine(HttpRuntime.AppDomainAppVirtualPath + "/", path.VirtualPath))
                    : HttpRuntime.AppDomainAppVirtualPath;

				var values = new Dictionary<string, object>
								 {
									 {
										 "controller", MVC.Auth.Name
									 },
									 {
										 "action", MVC.Auth.ActionNames.Login
									 },
									 {
										 MVC.Auth.LoginParams.returnUrl, returnUrl
									 },
									 {
										 MVC.Auth.LoginParams.username,
										 (currentUser != null) ? currentUser.UserName : null
									 },
									 {
										 MVC.Auth.LoginParams.error,
										 (int?)HttpStatusCode.Unauthorized
									 }
								 };

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(values));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private bool IsAuthorized(HttpContextBase httpContext)
        {
            var workContext = GetWorkContext();
            var currentUser = workContext.GetUser();
            return base.AuthorizeCore(httpContext) && workContext.IsAuthenticated() && currentUser != null;
        }
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            base.OnAuthorization(filterContext);
        }

    }
}