using System.Web.Mvc;

namespace VirtualGallery.Web.Extensions
{
    public class AjaxOrChildActionOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}