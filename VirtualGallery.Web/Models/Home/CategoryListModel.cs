using System;
using System.Collections.Generic;

namespace VirtualGallery.Web.Models.Home
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            Categories = new List<CategoryModel>();
        }

        public int PageNumber { get; set; }

        public IList<CategoryModel> Categories { get; set; }
    }
}
