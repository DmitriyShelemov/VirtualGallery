using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace VirtualGallery.Infrastructure.Localization
{
    internal class LocalizationManager
    {
        private const string ResourcesBaseName = "VirtualGallery.Infrastructure.Localization.Resourses.Localization";
        
        private static volatile LocalizationManager _instance;
        
        private static readonly object SyncRoot = new Object();

        private readonly ResourceManager _resourceManager;

        private readonly List<LocalizationSettings> _avalibleLocalizations;

        private bool _useTestLocalizationLanguage = false;

        private LocalizationManager()
        {            
            _resourceManager = new ResourceManager(ResourcesBaseName, Assembly.GetExecutingAssembly());
            
            _avalibleLocalizations = new List<LocalizationSettings>
            {
                 new LocalizationSettings { LanguageKey = LanguageKey.English, LocalizationName = LocalizationNames.English, CultureName = CultureNames.English, IsDefault = false, ResourceName = Localization.Names.Language_English },
                 new LocalizationSettings { LanguageKey = LanguageKey.Russian, LocalizationName = LocalizationNames.Russian, CultureName = CultureNames.Russian, IsDefault = true, ResourceName = Localization.Names.Language_Russian }
            };
        }

        public void AddTestLocalizationLanguage()
        {
            if (_useTestLocalizationLanguage) 
                return;

            var testLocalizationLanguage = new LocalizationSettings
                {
                    LanguageKey = LanguageKey.TestLocalization,
                    LocalizationName = LocalizationNames.TestLocalization,
                    CultureName = CultureNames.TestLocalization,
                    IsDefault = false,
                    ResourceName = Localization.Names.Language_English
                };
            _avalibleLocalizations.Add(testLocalizationLanguage);
            _useTestLocalizationLanguage = true;
        }

        public static LocalizationManager GetInstance()
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new LocalizationManager();
                    }
                }
            }
            return _instance;
        }

        public string GetString(string key)
        {
            if (_useTestLocalizationLanguage)
            {
                if (GetCurrentLocalization().LanguageKey == LanguageKey.TestLocalization)
                {
                    return GetTestLocalization(key);
                }
            }
            return _resourceManager.GetString(key);            
        }
        
        public  string GetString(string key, LanguageKey languageKey)
        {
            if (_useTestLocalizationLanguage)
            {
                if (languageKey == LanguageKey.TestLocalization)
                {
                    return GetTestLocalization(key);
                }
            }
            return _resourceManager.GetString(key, GetCultureInfo(languageKey));
        }

        private string GetTestLocalization(string key)
        {
            var text = _resourceManager.GetString(key);
            return PseudoLocalizer.Transform(text);
        }

        public List<LocalizationSettings> GetAvalibleLocalizations()
        {
            return _avalibleLocalizations;
        }

       public LocalizationSettings GetLocalizationByCulture(string cultureName)
        {
            return _avalibleLocalizations.FirstOrDefault(localizationSettings => localizationSettings.CultureName == cultureName);
        }

        public LocalizationSettings GetLocalizationByLanguageKey(LanguageKey languageKey)
        {
            return _avalibleLocalizations.FirstOrDefault(localizationSettings => localizationSettings.LanguageKey == languageKey);
        }

        public LocalizationSettings GetLocalizationByLocalizationName(string localizationName)
        {
            return _avalibleLocalizations.FirstOrDefault(localizationSettings => localizationSettings.LocalizationName == localizationName);
        }

        public LocalizationSettings GetCurrentLocalization()
        {
            var currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
            return GetLocalizationByCulture(currentCultureName) ??
                   GetDefaultLocalization();
        }

        public LocalizationSettings GetDefaultLocalization()
        {
            return _avalibleLocalizations.FirstOrDefault(localizationSettings => localizationSettings.IsDefault) ??
                    GetLocalizationByLanguageKey(LanguageKey.Russian);
        }

        public void SetCurrentLocalization(LocalizationSettings localization)
        {
            var cultureInfo = new CultureInfo(localization.CultureName);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }

        public CultureInfo GetCultureInfo(LanguageKey languageKey)
        {
            var localization = GetLocalizationByLanguageKey(languageKey);
            if (localization != null)
            {
                return CultureInfo.CreateSpecificCulture(localization.CultureName);
            }
            return CultureInfo.CreateSpecificCulture(GetDefaultLocalization().CultureName);
        }
    }
}
