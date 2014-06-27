using System.Collections.Generic;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class CartModel
    {
        public virtual IList<int> Pictures { get; set; }
    }
}
