namespace VirtualGallery.BusinessLogic.WorkContext
{
    public interface IWorkContext
    {
        UserInfo GetUser();
        
        bool IsAuthenticated();

        void LoginAs(UserInfo user);

		void UpdateCurrentUser(UserInfo user);

        void Logout();
    }
}
