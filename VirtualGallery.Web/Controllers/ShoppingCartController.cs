using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Orders.Interfaces;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Web.Extensions;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.ShoppingCart;

namespace VirtualGallery.Web.Controllers
{
    public partial class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _shoppingCartService;

        private readonly IPictureService _pictureService;

        private readonly IPreferenceService _preferenceService;

        public ShoppingCartController(IWorkContext workContext,
            IShoppingCartService shoppingCartService,
            IPictureService pictureService,
            IPreferenceService preferenceService)
            : base(workContext)
        {
            _shoppingCartService = shoppingCartService;
            _pictureService = pictureService;
            _preferenceService = preferenceService;
        }

        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult CartDialog(CartModel model)
        {
            var pictures = (model.Pictures != null && model.Pictures.Any()) 
                ? _pictureService.GetByIds(model.Pictures.ToArray()) : new List<Picture>();

            ViewBag.AllowEdit = CurrentUser != null;

            var pref = _preferenceService.Get();

            var orderModel = new OrderModel
            {
                SelfTakeAddress = pref.Address,
                Lots = pictures.Select(p => new LotModel
                {
                    PictureId = p.Id,
                    Picture = new CartPictureModel
                    {
                        Id = p.Id,
                        Details = p.Details,
                        Description = p.Description,
                        Name = p.Name,
                        Reserved = p.Reserved,
                        Sold = p.Sold,
                        ThumbnailUrl = p.Thumbnail.ResolveFilePath(),
                        Price = GetPicturePrice(p)
                    }
                }).ToList()
            };

            return View(MVC.ShoppingCart.Views._ShoppingCartDialog, orderModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult MakeOrder(OrderModel model)
        {
            if (model == null)
                return FailedJson(Localization.Hardcoded("Can't create an order."));

            if (!ModelState.IsValid)
            {
                return FailedJson(Localization.Hardcoded("Can't create an order."), fixUploadIE: true);
            }

            var pictures = (model.Lots != null && model.Lots.Any())
                ? _pictureService.GetByIds(model.Lots.Select(l => l.PictureId).ToArray()) : new List<Picture>();

            if (model.Lots == null 
                || pictures.Count != model.Lots.Count 
                || pictures.Any(p => p.Reserved || p.Sold))
                return FailedJson(Localization.Hardcoded("Can't create an order."));

            var order = new Order
            {
                From = model.Name,
                Email =  model.Email,
                Phone = model.Phone,
                DeliveryType = model.DeliveryType,
                Details = model.Details,
                Lots = pictures.Select(p => new Lot
                {
                    Picture = p,
                    Decor = model.Lots.First(l => l.PictureId == p.Id).Decor
                }).ToList()
            };

            _shoppingCartService.Add(order);

            return SuccessJson();
        }
        
        [AjaxOnly]
        [HttpGet]
        [GalleryAuthorize]
        public virtual ActionResult Orders()
        {
            ViewBag.AllowEdit = CurrentUser != null;

            var orders = _shoppingCartService.Get().ToList();
            var ordersModel = orders.Select(o => new OrderModel
            {
                Id = o.Id,
                Name = o.From,
                Email = o.Email,
                Phone = o.Phone,
                DeliveryType = o.DeliveryType,
                Details = o.Details,
                CreateDate = o.CreateDate
            }).ToList();

            return View(MVC.ShoppingCart.Views._OrdersDialog, ordersModel);
        }
        
        [AjaxOnly]
        [HttpGet]
        [GalleryAuthorize]
        public virtual ActionResult DropOrder(int orderId)
        {
            ViewBag.AllowEdit = CurrentUser != null;
            _shoppingCartService.Remove(_shoppingCartService.GetById(orderId));            
            return SuccessJson();
        }

        [AjaxOnly]
        [HttpGet]
        public virtual ActionResult DecorDialog()
        {
            ViewBag.AllowEdit = CurrentUser != null;

            var pref = _preferenceService.Get();
            return View(MVC.ShoppingCart.Views._DecorDialog, new DecorModel
            {
                SimpleBoxText = pref.DecorSimpleText,
                FrameText = pref.DecorFrameText,
                LuxText = pref.DecorLuxText
            });
        }

        [AjaxOnly]
        [HttpPost]
        [GalleryAuthorize]
        public virtual ActionResult DecorDialog(DecorModel model)
        {
            ViewBag.AllowEdit = CurrentUser != null;

            var pref = _preferenceService.Get();
            pref.DecorSimpleText = model.SimpleBoxText;
            pref.DecorFrameText = model.FrameText;
            pref.DecorLuxText = model.LuxText;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }

        private static string GetPicturePrice(Picture p)
        {
            switch (Localization.GetCurrentLocalization().LanguageKey)
            {
                case LanguageKey.English:
                    return string.Format("{0:C}", p.PriceEuro);
                case LanguageKey.TestLocalization:
                    return string.Format("{0:C}", p.PriceDollar);
                case LanguageKey.Russian:
                default:
                    return string.Format("{0:C}", p.PriceRouble);
            }
        }
    }
}
