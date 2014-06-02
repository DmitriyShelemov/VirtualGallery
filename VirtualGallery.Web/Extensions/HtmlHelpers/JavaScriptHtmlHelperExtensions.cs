using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;

namespace VirtualGallery.Web.Extensions.HtmlHelpers
{
    public static class JavaScriptHtmlHelperExtensions
    {
        public static MvcHtmlString AsJson(this HtmlHelper htmlHelper, object model)
        {
            var serializer = new JsonSerializer();
            
            return MvcHtmlString.Create(serializer.SerializeToString(model));
        }
        
        public static MvcHtmlString RenderJsStringView(this HtmlHelper htmlHelper, string viewName, object model)
        {
            string viewContent = htmlHelper.Partial(viewName, model).ToString();
            return new MvcHtmlString(viewContent.Replace(Environment.NewLine, string.Empty));
        }

    }
}