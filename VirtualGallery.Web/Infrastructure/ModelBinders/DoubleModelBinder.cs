using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace VirtualGallery.Web.Infrastructure.ModelBinders
{
	public class DoubleModelBinder : LocalizedDefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			var modelState = new ModelState { Value = valueResult };
			object actualValue = null;
			try
			{
				actualValue = double.Parse(valueResult.AttemptedValue, NumberStyles.Float, Thread.CurrentThread.CurrentCulture);
			}
			catch (FormatException e)
			{
				modelState.Errors.Add(e);
			}

			bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
			return actualValue;
		}
	}
}