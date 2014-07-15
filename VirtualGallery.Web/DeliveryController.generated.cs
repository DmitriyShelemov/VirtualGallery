// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace VirtualGallery.Web.Controllers
{
    public partial class DeliveryController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DeliveryController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SetDeliverySummary()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetDeliverySummary);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SetDeliveryTypeSummary()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetDeliveryTypeSummary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DeliveryController Actions { get { return MVC.Delivery; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Delivery";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Delivery";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string SetDeliverySummary = "SetDeliverySummary";
            public readonly string SetDeliveryTypeSummary = "SetDeliveryTypeSummary";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string SetDeliverySummary = "SetDeliverySummary";
            public const string SetDeliveryTypeSummary = "SetDeliveryTypeSummary";
        }


        static readonly ActionParamsClass_SetDeliverySummary s_params_SetDeliverySummary = new ActionParamsClass_SetDeliverySummary();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetDeliverySummary SetDeliverySummaryParams { get { return s_params_SetDeliverySummary; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetDeliverySummary
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_SetDeliveryTypeSummary s_params_SetDeliveryTypeSummary = new ActionParamsClass_SetDeliveryTypeSummary();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetDeliveryTypeSummary SetDeliveryTypeSummaryParams { get { return s_params_SetDeliveryTypeSummary; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetDeliveryTypeSummary
        {
            public readonly string deliveryType = "deliveryType";
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Index = "Index";
            }
            public readonly string Index = "~/Views/Delivery/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_DeliveryController : VirtualGallery.Web.Controllers.DeliveryController
    {
        public T4MVC_DeliveryController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SetDeliverySummaryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, VirtualGallery.Web.Models.Home.HtmlModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult SetDeliverySummary(VirtualGallery.Web.Models.Home.HtmlModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetDeliverySummary);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SetDeliverySummaryOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void SetDeliveryTypeSummaryOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, VirtualGallery.BusinessLogic.Orders.DeliveryType deliveryType, VirtualGallery.Web.Models.Home.HtmlModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult SetDeliveryTypeSummary(VirtualGallery.BusinessLogic.Orders.DeliveryType deliveryType, VirtualGallery.Web.Models.Home.HtmlModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SetDeliveryTypeSummary);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "deliveryType", deliveryType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            SetDeliveryTypeSummaryOverride(callInfo, deliveryType, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
