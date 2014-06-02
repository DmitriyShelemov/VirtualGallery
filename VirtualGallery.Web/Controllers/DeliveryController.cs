using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Delivery;

namespace VirtualGallery.Web.Controllers
{
    public partial class DeliveryController : BaseController
    {
        public DeliveryController(IWorkContext workContext)
            : base(workContext)
        {
        }

        public virtual ActionResult Index()
        {
            return View(new DeliveryPageModel());
        }

    }
}
