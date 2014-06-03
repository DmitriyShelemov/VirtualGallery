using System.ComponentModel.DataAnnotations;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.Web.Models.Contact
{
    public class FeedbackModel
    {
        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Required)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Email)]
        [StringLength(128, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Contact_FromEmail, Prompt = Localization.Names.Contact_FromEmail)]
        public string FromEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Required)]
        [StringLength(512, ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_Validation_Max_Length, MinimumLength = 1)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Contact_Body, Prompt = Localization.Names.Contact_Body)]
        public string Body { get; set; }
    }
}