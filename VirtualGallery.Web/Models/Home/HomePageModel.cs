namespace VirtualGallery.Web.Models.Home
{
    public class HomePageModel: BasePageModel
    {
        public HomePageModel()
        {
            SelectedTab = MainMenuKey.Main;
        }

        public string Description { get; set; }
    }
}