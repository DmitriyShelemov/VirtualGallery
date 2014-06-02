using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Infrastructure.Logging;
using VirtualGallery.Web.Infrastructure.ActionResults;
using VirtualGallery.Web.Infrastructure.ClientSide;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Models;

namespace VirtualGallery.Web.Infrastructure.Presentation
{
    /// <summary>
    /// Base controller with Authorization
    /// </summary>
    public class BaseController : Controller
    {
        private readonly IWorkContext _workContext;

        public BaseController()
            : this(null)
        {
        }

        public BaseController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                return WorkContext.GetUser();
            }
        }

        protected JsonResult FailedCaptchaJson()
        {
            return Json(
                new AjaxRequestResult
                    {
                        Success = false,
                        CaptchaValid = false,
                        ErrorMessage = Localization.RemindPassword_WrongCaptcha
                    });
        }

        protected JsonResult FailedJson(string errorMessage, object data = null, bool fixUploadIE = false)
        {
            var result = Json(new AjaxRequestResult { Success = false, Data = data, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
            return fixUploadIE ? ContentTypeIEFix(result) : result;
        }

        protected override JsonResult Json(
            object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new GalleryJsonResult
                       {
                           Data = data,
                           ContentType = contentType,
                           ContentEncoding = contentEncoding,
                           JsonRequestBehavior = behavior
                       };
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Localization.SetCurrentLocalization(Localization.GetDefaultLocalization());
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;

            if (ex is AuthorizationException && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
            }
            else if (ex is LocalizedValidationException && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = FailedJson(ex.Message);
                filterContext.ExceptionHandled = true;
            }
            else if (ex is LocalizedValidationException)
            {
                filterContext.Result = View(MVC.Shared.Views.Error, new ErrorModel { Message = ex.Message });
                filterContext.ExceptionHandled = true;
            }
            else if (ex is HttpAntiForgeryException)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = FailedJson(Localization.Security_Session_has_expired);
                }
                else
                {
                    if (filterContext.RouteData.Values["Action"].ToString() == MVC.Auth.ActionNames.Login)
                    {
                        var requestParams = filterContext.RequestContext.HttpContext.Request.Params;
                        filterContext.Result = RedirectToAction(
                            MVC.Auth.ActionNames.Login,
                            MVC.Auth.Name,
                            new { returnUrl = requestParams["returnUrl"], error = 401, username = requestParams["UserName"] });
                    }
                    else
                    {
                        filterContext.Result = RedirectToRoute(filterContext.RouteData.Route);
                    }

                }
                filterContext.ExceptionHandled = true;
            }
            else if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                Logger.Instance.WriteLog(Localization.Common_Error_Message, ex, LogLevel.Error);
                filterContext.Result = FailedJson(Localization.Common_Error_Message);
                filterContext.ExceptionHandled = true;
            }
            else
            {
                Logger.Instance.WriteLog(Localization.Common_Error_Message, ex, LogLevel.Error);
                filterContext.Result = View(MVC.Shared.Views.Error, new ErrorModel { Message = Localization.Common_Error_Message });
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }

        protected JsonResult SuccessJson(object data = null, bool fixUploadIE = false)
        {
            var result = Json(new AjaxRequestResult { CaptchaValid = true, Success = true, Data = data }, JsonRequestBehavior.AllowGet);
            return fixUploadIE ? ContentTypeIEFix(result) : result;
        }

        protected ActionResult JsonRedirectToAction(ActionResult action)
        {
            return SuccessJson(
                new AjaxRequestResultDataCommand
                {
                    CommandName = AjaxRequestResultDataCommand.RedirectToPage,
                    Data = Url.Action(action)
                });
        }

        protected void ThrowAccessDenied()
        {
            throw new AuthorizationException();
        }

        protected internal static string GetPageTitle(string topMenuTabName, string section = null)
        {
            var siteTitle = Localization.Common_Gallery + " " + topMenuTabName;
            return string.IsNullOrEmpty(section) ? siteTitle : siteTitle + " - " + section;
        }


        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        private JsonResult ContentTypeIEFix(JsonResult result)
        {
            // Fix for IE <= 9. Content type should be "text/html" for iframe transport
            var acceptTypes = ControllerContext.HttpContext.Request.AcceptTypes;
            if (acceptTypes == null || !acceptTypes.Contains("application/json"))
            {
                result.ContentType = "text/html";
            }
            return result;
        }
    }
}