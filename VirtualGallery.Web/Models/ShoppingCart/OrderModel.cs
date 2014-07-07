using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class OrderModel: BaseOrderModel
    {        
        public string SelfTakeAddress { get; set; }

        public virtual IList<LotModel> Lots { get; set; }
    }
}
