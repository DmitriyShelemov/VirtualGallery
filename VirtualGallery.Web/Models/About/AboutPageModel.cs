namespace VirtualGallery.Web.Models.About
{
    public class AboutPageModel : BasePageModel
    {
        public AboutPageModel()
        {
            SelectedTab = MainMenuKey.About;
        }

        public string PhotoUrl { get; set; }

        public string About { get; set; }

        public string About2 { get; set; }
    }
}