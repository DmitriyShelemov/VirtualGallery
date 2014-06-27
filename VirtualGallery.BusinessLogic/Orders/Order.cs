using System;
using System.Collections.Generic;

namespace VirtualGallery.BusinessLogic.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Details { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public bool Completed { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual IList<Lot> Lots { get; set; }
    }
}
