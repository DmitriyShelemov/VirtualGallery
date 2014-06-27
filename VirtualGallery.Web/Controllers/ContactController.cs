using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.EMail;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.EMail.Messages;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Web.Extensions;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Contact;

namespace VirtualGallery.Web.Controllers
{
    public partial class ContactController : BaseController
    {
        private readonly IMailBox _mailBox;

        private readonly IPreferenceService _preferenceService;

        public ContactController(IWorkContext workContext, IMailBox mailBox, IPreferenceService preferenceService)
            : base(workContext)
        {
            _mailBox = mailBox;
            _preferenceService = preferenceService;
        }

        public virtual ActionResult Index()
        {
            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();
            return View(new ContactPageModel
            {
                Contacts = new ContactsModel
                {
                    Address = string.IsNullOrEmpty(pref.Address) ? Localization.Contact_Address_i : pref.Address,
                    MapUrl = string.IsNullOrEmpty(pref.Address) ? @"//api-maps.yandex.ru/services/constructor/1.0/js/?sid=o0PhGpqM3JDsh7xRxnMN48pECc5wBQg1" : pref.MapUrl,
                    Phone = string.IsNullOrEmpty(pref.Phone) ? Localization.Contact_Phone_i : pref.Phone,
                    WorkTime = string.IsNullOrEmpty(pref.WorkTime) ? Localization.Contact_Business_Hours_i : pref.WorkTime,
                }
            });
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetContacts(ContactsModel model)
        {
            var pref = _preferenceService.Get();
            pref.Address = model.Address;
            pref.Phone = model.Phone;
            pref.WorkTime = model.WorkTime;
            pref.MapUrl = model.MapUrl;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }

        [ChildActionOnly]
        public virtual ActionResult SendFeedback()
        {
            return View(MVC.Contact.Views._Feedback, new FeedbackModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SendFeedback(FeedbackModel model)
        {
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                return FailedCaptchaJson();
            }

            if (!ModelState.IsValid)
            {
                return FailedJson("Unable to send feedback", fixUploadIE: true);
            }

            _mailBox.Send(new Message(AppSettings.MailFrom, model.FromEmail, model.Body) { From = model.FromEmail });

            return SuccessJson();
        }
    }
}
