namespace VirtualGallery.Web.Infrastructure.ClientSide
{
    public class AjaxRequestResult
    {
        public string ErrorMessage { get; set; }

        public string FeedbackMessage { get; set; }

        public bool? Success { get; set; }

        public bool? CaptchaValid { get; set; }

        public object Data { get; set; }
    }
}