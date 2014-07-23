using System.Web.Mvc;
using MvcSiteMapProvider;
using VirtualGallery.BusinessLogic.Pictures;
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

        [MvcSiteMapNode(Title = NameConst, Key = NameConst, ParentKey = HomeController.NameConst, UpdatePriority = UpdatePriority.Absolute_050)]
        public virtual ActionResult Index()
        {
            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();

            return View(new AboutPageModel
            {
                Title = Localization.Nav_Main_Title,
                About = pref.About,
                About2 = pref.About2,
                PhotoUrl = pref.Photo != null ? pref.Photo.ResolveFilePath() : string.Empty
            });
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetAbout(HtmlModel model)
        {
            var pref = _preferenceService.Get();
            pref.About = model.Text;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetAbout2(HtmlModel model)
        {
            var pref = _preferenceService.Get();
            pref.About2 = model.Text;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
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
