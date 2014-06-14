using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Web.Extensions;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.About;
using VirtualGallery.Web.Models.Home;

namespace VirtualGallery.Web.Controllers
{
    public partial class AboutController : BaseController
    {
        private readonly IPreferenceService _preferenceService;

        public AboutController(IWorkContext workContext, IPreferenceService preferenceService)
            : base(workContext)
        {
            _preferenceService = preferenceService;
        }

        public virtual ActionResult Index()
        {
            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();

            return View(new AboutPageModel
            {
                Title = Localization.Nav_Main_Title,
                About = pref.About,
                PhotoUrl = pref.Photo != null ? pref.Photo.ResolveFilePath() : string.Empty
            });
        }

        [GalleryAuthorize]
        [HttpPost]
        public virtual ActionResult Upload(PhotoUploadButtonModel model)
        {
                if (model.File != null && model.File.ContentLength <= 0)
                    return FailedJson(Localization.Common_Error_File_Has_No_Content, fixUploadIE: true);

                if (model.File != null && model.File.ContentLength > Picture.MaxFileSize)
                    return FailedJson(string.Format(Localization.Picture_BigFileSize, model.File.FileName), fixUploadIE: true);

                _preferenceService.SetPhoto(model.File);

                return SuccessJson(null, true);
        }
    }
}
