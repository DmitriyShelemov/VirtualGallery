using System.Web.Mvc;

namespace VirtualGallery.Web.Models.Shared
{
	public class HorizontalListSelectorModel
	{
		public bool ShowText { get; set; }

        public bool DisableTooltip { get; set; }

		public string Prefix { get; set; }

		public string StylePrefix { get; set; }

        public string SelectedValue { get; set; }

        public SelectList Items { get; set; }
    }
}