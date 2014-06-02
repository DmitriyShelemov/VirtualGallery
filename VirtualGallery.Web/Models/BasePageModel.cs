using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualGallery.Web.Models
{
    public class BasePageModel
    {
        public MainMenuKey SelectedTab { get; set; }

        public string Title { get; set; }
    }
}