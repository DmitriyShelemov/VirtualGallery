namespace VirtualGallery.Web.Models.Home
{
    public class CategoryPageModel : HomePageModel
    {
        public CategoryPageModel()
        {
            SelectedTab = MainMenuKey.Main;
        }

        public CategoryModel Category { get; set; }
    }
}