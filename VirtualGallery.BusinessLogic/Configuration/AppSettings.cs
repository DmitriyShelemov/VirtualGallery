using System.Configuration;
namespace VirtualGallery.BusinessLogic.Configuration
{
    public static class AppSettings
    {
        private static string _mailFrom;

        private static string _timeZone;

        public static string MailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(_mailFrom))
                {
                    _mailFrom = GetSettingAsString("MailFrom", "dmitryshelemov@gmail.com");
                }

                return _mailFrom;
            }
        }

        public static string TimeZone
        {
            get { return _timeZone ?? (_timeZone = ConfigurationManager.AppSettings["TimeZone"]); }
        }

        public static string AdminName
        {
            get { return GetSettingAsString("AdminName", "Admin"); }
        }

        public static string AdminPwd
        {
            get { return GetSettingAsString("AdminPwd", "Password"); }
        }

        public static string YaMetrika
        {
            get { return GetSettingAsString("YaMetrika", "0"); }
        }

        public static string FileStorageBaseUrl
        {
            get { return GetSettingAsString("FileStorageBaseUrl", "~/files"); }
        }

        public static string FileStorageBasePath
        {
            get { return GetSettingAsString("FileStorageBasePath", ""); }
        }

        public static int PageSize
        {
            get { return GetSettingAsNumber("PageSize", 5); }
        }

        private static string GetSettingAsString(string settingName, string defaultValue = null)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];
            if (defaultValue != null && string.IsNullOrEmpty(settingValue))
            {
                settingValue = defaultValue;
            }
            return settingValue;
        }

        private static int GetSettingAsNumber(string settingName, int defaultValue)
        {
            var settingValue = ConfigurationManager.AppSettings[settingName];
            int val;
            int.TryParse(settingValue, out val);
            return int.TryParse(settingValue, out val) ? val : defaultValue;
        }
    }
}