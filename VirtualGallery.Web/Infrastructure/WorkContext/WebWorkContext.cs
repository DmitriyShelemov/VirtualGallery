using System.Web;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Web.Infrastructure.State;

namespace VirtualGallery.Web.Infrastructure.WorkContext
{
    public class WebWorkContext : IWorkContext
    {
        private readonly SessionStateManager _sessionStateManager = SessionStateManager.GetInstance();

        private UserInfo _currentUser;

        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated && GetUser() != null;
        }

        public UserInfo GetUser()
        {
            return _currentUser ?? (_currentUser = _sessionStateManager.CurrentUserInfo);
        }

        public void LoginAs(UserInfo user)
        {
            _sessionStateManager.CurrentUserInfo = user;
            _currentUser = null;
        }

        public void UpdateCurrentUser(UserInfo user)
        {
            LoginAs(user);
        }

        public void Logout()
        {
            _sessionStateManager.CurrentUserInfo = null;
            _currentUser = null;
			HttpContext.Current.Session.Abandon();
        }
    }
}