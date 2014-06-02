using System.Transactions;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.DataAccess.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextProvider _dbContextProvider;

        public UnitOfWorkFactory(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public IUnitOfWork Create(bool autoDetectChanges = true)
        {
            return new UnitOfWork(_dbContextProvider, autoDetectChanges);
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel, bool autoDetectChanges = true)
        {
            return new UnitOfWork(_dbContextProvider, isolationLevel, autoDetectChanges);
        }
    }
}