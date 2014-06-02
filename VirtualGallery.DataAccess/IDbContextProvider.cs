using System;

namespace VirtualGallery.DataAccess
{
    public interface IDbContextProvider : IDisposable
    {
        VirtualGalleryDbContext GetDbContext();        
    }
}
