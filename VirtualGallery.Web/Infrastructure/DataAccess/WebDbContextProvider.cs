using System;
using System.Configuration;
using VirtualGallery.DataAccess;

namespace VirtualGallery.Web.Infrastructure.DataAccess
{
    public class WebDbContextProvider : IDbContextProvider
    {
        private VirtualGalleryDbContext _dbContext;

        public WebDbContextProvider()
        {
            _dbContext = new VirtualGalleryDbContext(ConfigurationManager.ConnectionStrings["VirtualGallery"].ConnectionString);
        }

        public VirtualGalleryDbContext GetDbContext()
        {
            return _dbContext;
        }

        public void Dispose()
        {
            Dispose(true);

            // Call GC.SupressFinalize to take this object off the finalization queue 
            // and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}