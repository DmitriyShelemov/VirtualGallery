using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using VirtualGallery.Web.Extensions;

namespace VirtualGallery.Web.Infrastructure.ActionResults
{
    public class GalleryJsonResult : JsonResult
    {
        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param><exception cref="T:System.ArgumentNullException">The <paramref name="context"/> parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                // base class will throw localized exception
                base.ExecuteResult(context);
            }
            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data == null)
            {
                return;
            }
            
            var serializer = new JsonSerializer();

            response.Write(serializer.SerializeToString(Data));
        }
    }
}