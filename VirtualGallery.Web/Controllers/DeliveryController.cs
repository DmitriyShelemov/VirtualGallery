using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Web.Extensions;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Delivery;
using VirtualGallery.Web.Models.Home;

namespace VirtualGallery.Web.Controllers
{
    public partial class DeliveryController : BaseController
    {
        private readonly IPreferenceService _preferenceService;

        public DeliveryController(IWorkContext workContext, IPreferenceService preferenceService)
            : base(workContext)
        {
            _preferenceService = preferenceService;
        }

        [MvcSiteMapNode(Title = NameConst, Key = NameConst, ParentKey = HomeController.NameConst, UpdatePriority = UpdatePriority.Absolute_050)]
        public virtual ActionResult Index()
        {
            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();
            return View(new DeliveryPageModel
            {
                DeliverySummary = pref.DeliverySummary,
                DeliveryExpressSummary = pref.DeliveryExpressSummary,
                DeliverySelfTakeSummary = pref.DeliverySelfTakeSummary
            });
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetDeliverySummary(HtmlModel model)
        {
            var pref = _preferenceService.Get();
            pref.DeliverySummary = model.Text;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetDeliveryTypeSummary(DeliveryType deliveryType, HtmlModel model)
        {
            var pref = _preferenceService.Get();
            switch (deliveryType)
            {
                case DeliveryType.Express:
                    pref.DeliveryExpressSummary = model.Text;
                    break;
                case DeliveryType.SelfTake:
                    pref.DeliverySelfTakeSummary = model.Text;
                    break;
            }
            
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }
    }
}
