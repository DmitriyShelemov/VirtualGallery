using System;
using System.Collections.Generic;
using VirtualGallery.BusinessLogic.Pictures;

namespace VirtualGallery.BusinessLogic.Categories
{
    public class Category
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual IList<Picture> Pictures { get; set; }

        public bool Deleted { get; set; }
    }
}
