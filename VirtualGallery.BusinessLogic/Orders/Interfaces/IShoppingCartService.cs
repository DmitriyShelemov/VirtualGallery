using System.Collections.Generic;

namespace VirtualGallery.BusinessLogic.Orders.Interfaces
{
    public interface IShoppingCartService
    {
        void Add(Order order);

        void Update(Order order);

        void Remove(Order order);

        IList<Order> Get();

        Order GetById(int orderId);
    }
}
