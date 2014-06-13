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
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Contact;

namespace VirtualGallery.Web.Controllers
{
    public partial class ContactController : BaseController
    {
        private readonly IMailBox _mailBox;

        public ContactController(IWorkContext workContext, IMailBox mailBox)
            : base(workContext)
        {
            _mailBox = mailBox;
        }

        public virtual ActionResult Index()
        {
            return View(new ContactPageModel());
        }

        [ChildActionOnly]
        public virtual ActionResult SendFeedback()
        {
            return View(MVC.Contact.Views._Feedback, new FeedbackModel());
        }

        [HttpPost]
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
