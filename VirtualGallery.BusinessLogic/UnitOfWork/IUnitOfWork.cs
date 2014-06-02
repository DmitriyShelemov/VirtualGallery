using System;

namespace VirtualGallery.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();

        void Commit();
    }
}
