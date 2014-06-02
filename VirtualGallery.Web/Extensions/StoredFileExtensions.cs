using System.Web;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;

namespace VirtualGallery.Web.Extensions
{
	public static class StoredFileExtensions
	{
		public static string ResolveFilePath(this StoredFile file)
		{
			var context = HttpContext.Current;
			return context == null
					   ? null
					   : UrlHelper.GenerateContentUrl(DependencyResolver.Current.GetService<IFileStorage>().GetFileUrl(file), new HttpContextWrapper(context));
		}

		public static string ResolvePhysicalFilePath(this StoredFile file)
		{
			var context = HttpContext.Current;
			return context == null
					   ? null
					   : DependencyResolver.Current.GetService<IFileStorage>().GetFilePhysicalPath(file);
		}
	}
}