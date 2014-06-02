using System.Transactions;

namespace VirtualGallery.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(bool autoDetectChanges = true);
        IUnitOfWork Create(IsolationLevel isolationLevel, bool autoDetectChanges = true);
    }
}
