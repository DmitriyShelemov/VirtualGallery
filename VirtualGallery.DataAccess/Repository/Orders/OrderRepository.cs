using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Orders.Interfaces;

namespace VirtualGallery.DataAccess.Repository.Orders
{
    public class OrderRepository : BaseRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}
