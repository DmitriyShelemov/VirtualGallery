using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.BusinessLogic.Preferences
{
    public class PreferenceService : BaseService, IPreferenceService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IPreferenceRepository _preferenceRepository;

        private readonly IFileStorage _fileStorage;

        public PreferenceService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IPreferenceRepository preferenceRepository,
            IFileStorage fileStorage)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _preferenceRepository = preferenceRepository;
            _fileStorage = fileStorage;
        }

        public void SetPhoto(HttpPostedFileBase photo)
        {
            ValidateFileType(photo.FileName);

            CreateOrUpdatePreference(p =>
            {
                p.Photo = SaveFile(photo.InputStream, photo.FileName);
            });
        }

        public void Update(Preference preference)
        {
            CreateOrUpdatePreference(p =>
            {
                p.Intro = preference.Intro;
                p.About = preference.About;
            });
        }

        public Preference Get()
        {
            return _preferenceRepository.Get() ?? new Preference();
        }

        private void CreateOrUpdatePreference(Action<Preference> action)
        {
            var pref = _preferenceRepository.Get();
            if (pref != null)
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    action(pref);
                    _preferenceRepository.Update(pref);
                    unitOfWork.Commit();
                }
            }
            else
            {
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    pref = new Preference();
                    action(pref);
                    _preferenceRepository.Add(pref);
                    unitOfWork.Commit();
                }
            }
        }

        private void ValidateFileType(string fileName)
        {
            if (!new Regex("\\.(?i:bmp|gif|jpe?g|png)$").Match(fileName).Success)
                throw new LocalizedValidationException(string.Format(Localization.Picture_TypeNotSupported_Template, Localization.Picture_SupportedTypes_Image));
        }

        private StoredFile SaveFile(Stream fileStream, string fileName)
        {
            var tempFolder = _fileStorage.GetFilePhysicalPath(DefaultDirectories.Preference);
            FileStorage.EnsureDirectoryExists(tempFolder);
            return _fileStorage.SaveFile(fileStream, fileName, DefaultDirectories.Preference, true);
        }
    }
}
