using System.Web.Mvc;
using Newtonsoft.Json;
using VirtualGallery.Web.Infrastructure.ModelBinders;

namespace VirtualGallery.Web.Extensions
{
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

		private class JsonModelBinder : LocalizedDefaultModelBinder
        {
			public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
				return BindModel(() =>
				{
					var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                    return string.IsNullOrEmpty(stringified) ? null : JsonConvert.DeserializeObject(stringified, bindingContext.ModelType);
				});
            }
        }
    }
}