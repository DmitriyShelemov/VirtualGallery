using System;
using System.Web;

namespace VirtualGallery.Web.Infrastructure.Cookies
{
    internal class CookieStateManager
    {
        private static volatile CookieStateManager _instance;
        
        private static readonly object SyncRoot = new object();

        private CookieStateManager()
        {}
        
        public static CookieStateManager GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new CookieStateManager();
                    }
                }
            }

            return _instance;
        }
    }
}