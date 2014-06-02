namespace VirtualGallery.Web.Infrastructure.ClientSide
{
    public class AjaxRequestResultDataCommand
    {
        #region  Constants

        public const string UpdateFilter = "UpdateFilter";

        public const string ReloadPage = "ReloadPage";

        public const string RedirectToPage = "RedirectTo";

        #endregion //Constants

        public string CommandName { get; set; }

        public object Data { get; set; }
    }
}