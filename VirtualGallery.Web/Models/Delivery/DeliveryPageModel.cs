namespace VirtualGallery.Web.Models.Delivery
{
    public class DeliveryPageModel : BasePageModel
    {
        public DeliveryPageModel()
        {
            SelectedTab = MainMenuKey.Delivery;
        }

        public string DeliverySummary { get; set; }

        public string DeliveryExpressSummary { get; set; }

        public string DeliverySelfTakeSummary { get; set; }
    }
}