using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class OrderModel
    {
        public int Id { get; set; }

        [StringLength(128, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Common_Name, Prompt = Localization.Names.Common_Name)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Required)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Email)]
        [StringLength(128, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Contact_Mail, Prompt = Localization.Names.Contact_Mail)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Required)]
        [StringLength(128, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Contact_Phone, Prompt = Localization.Names.Contact_Phone)]
        public string Phone { get; set; }

        [StringLength(1024, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Common_Details, Prompt = Localization.Names.Common_Details)]
        public string Details { get; set; }

        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Common_Delivery_Type, Prompt = Localization.Names.Common_Delivery_Type)]
        public DeliveryType DeliveryType { get; set; } 
        
        public string SelfTakeAddress { get; set; }

        public virtual IList<LotModel> Lots { get; set; }
    }
}
