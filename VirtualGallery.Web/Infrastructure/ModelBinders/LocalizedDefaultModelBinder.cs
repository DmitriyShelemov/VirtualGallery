using System;
using System.Web;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.Web.Infrastructure.ModelBinders
{
	public class LocalizedDefaultModelBinder : DefaultModelBinder
	{
		private const int EDangeroushtml = unchecked((int)0x80004005);

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return BindModel(() => base.BindModel(controllerContext, bindingContext));
		}

		protected object BindModel(Func<object> action)
		{
			try
			{
				return action();
			}
			catch (HttpRequestValidationException ex)
			{
				if (ex.ErrorCode == EDangeroushtml)
				{
					throw new LocalizedValidationException(Localization.Common_Error_Dangerous_Html_Message, ex);
				}

				throw new LocalizedValidationException(Localization.Common_Error_Message, ex);
			}
			catch (Exception ex)
			{
				if (!(ex is LocalizedValidationException))
				{
					throw new LocalizedValidationException(Localization.Common_Error_Message, ex);
				}

				throw;
			}
		}

	}
}