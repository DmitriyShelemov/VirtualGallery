using System;
using System.Collections.Generic;
using System.Web;
using VirtualGallery.BusinessLogic.WorkContext;

namespace VirtualGallery.Web.Infrastructure.State
{
    internal class SessionStateManager
    {
        public static class SessionKey 
        {
            public const string CurrentUser = "CURRENT_USER_INFO";
        }

        private static volatile SessionStateManager _instance;
        
        private static readonly object SyncRoot = new object();

        private SessionStateManager()
        {}

        public UserInfo CurrentUserInfo
        {
            get
            {
                return HttpContext.Current.Session[SessionKey.CurrentUser] as UserInfo;
            }
            set
            {
                HttpContext.Current.Session[SessionKey.CurrentUser] = value;
            }
        }

        public static SessionStateManager GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionStateManager();
                    }
                }
            }

            return _instance;
        }
    }
}