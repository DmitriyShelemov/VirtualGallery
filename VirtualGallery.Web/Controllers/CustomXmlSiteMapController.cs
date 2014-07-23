//using System;
//using System.Web.Mvc;
//using MvcSiteMapProvider.Web.Mvc;
//using VirtualGallery.Web.Infrastructure.Presentation;

//namespace VirtualGallery.Web.Controllers
//{
//    [AllowAnonymous]
//    public partial class CustomXmlSiteMapController : BaseController
//    {
//        private readonly IXmlSiteMapResultFactory _xmlSiteMapResultFactory;

//        public CustomXmlSiteMapController(IXmlSiteMapResultFactory xmlSiteMapResultFactory)
//        {
//            if (xmlSiteMapResultFactory == null)
//                throw new ArgumentNullException("xmlSiteMapResultFactory");
//            this._xmlSiteMapResultFactory = xmlSiteMapResultFactory;
//        }


//        public virtual ActionResult Index(int page = 0)
//        {
//            return _xmlSiteMapResultFactory.Create(page, new[] { "mainNavigation", "footerNavigation" });
//        }
//    }
//}
