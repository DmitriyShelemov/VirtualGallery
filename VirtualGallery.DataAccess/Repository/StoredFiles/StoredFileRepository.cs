using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;

namespace VirtualGallery.DataAccess.Repository.StoredFiles
{
    public class StoredFileRepository : BaseRepository<StoredFile, int>, IStoredFileRepository
    {
        public StoredFileRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {}
    }
}

