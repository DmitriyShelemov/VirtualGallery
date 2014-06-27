using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Orders.Interfaces;

namespace VirtualGallery.DataAccess.Repository.Orders
{
    public class LotRepository : BaseRepository<Lot, int>, ILotRepository
    {
        public LotRepository(IDbContextProvider dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}
