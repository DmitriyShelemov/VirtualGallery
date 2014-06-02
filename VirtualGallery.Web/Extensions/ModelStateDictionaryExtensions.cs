using System.Linq;
using System.Web.Mvc;

namespace VirtualGallery.Web.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string CollectErrorsAsStrings(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors).Aggregate("", (current, error) => current + (error.ErrorMessage + "\n"));
        }
    }
}