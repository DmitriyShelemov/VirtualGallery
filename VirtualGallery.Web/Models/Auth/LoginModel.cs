using System.ComponentModel.DataAnnotations;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.Web.Models.Auth
{
	public class LoginModel
	{
		public LoginModel()
		{
			Title = Localization.Common_Gallery;
		}

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_The_X_field_is_required)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Common_Username)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Localization), ErrorMessageResourceName = Localization.Names.Common_The_X_field_is_required)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Localization), Name = Localization.Names.Common_Password, Prompt = Localization.Names.Common_Password)]
        public string Password { get; set; }

		public string ReturnUrl { get; set; }

        public string Title { get; set; }

		public bool Expired { get; set; }
    }
}
