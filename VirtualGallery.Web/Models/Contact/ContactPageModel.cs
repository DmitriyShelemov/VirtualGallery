﻿namespace VirtualGallery.Web.Models.Contact
{
    public class ContactPageModel : BasePageModel
    {
        public ContactPageModel()
        {
            SelectedTab = MainMenuKey.Contact;
        }

        public ContactsModel Contacts { get; set; }
    }
}