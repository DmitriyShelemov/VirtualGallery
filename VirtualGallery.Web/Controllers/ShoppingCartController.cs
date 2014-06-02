using System.Web.Mvc;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.ShoppingCart;

namespace VirtualGallery.Web.Controllers
{
    public partial class ShoppingCartController : BaseController
    {
        public ShoppingCartController(IWorkContext workContext)
            : base(workContext)
        {
        }

        public virtual ActionResult Index()
        {
            return View(new ShoppingCartPageModel());
        }

    }
}
