using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.Filtering;
using VirtualGallery.BusinessLogic.Filtering.Sorting;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.BusinessLogic.Pictures
{
    public class PictureService : BaseService, IPictureService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IPictureRepository _pictureRepository;

        private readonly IFileStorage _fileStorage;

        public PictureService(
            IUnitOfWorkFactory unitOfWorkFactory, 
            IPictureRepository pictureRepository,
            IFileStorage fileStorage)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _pictureRepository = pictureRepository;
            _fileStorage = fileStorage;
        }

        public Picture Add(int categoryId, HttpPostedFileBase pictureFile)
        {
            EnsureValidId(categoryId, "categoryId");

            ValidateFileType(pictureFile.FileName);

            var pictureName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            var picture = new Picture
            {
                Name = pictureName,
                Description = pictureName,
                CategoryId = categoryId,
                Topic = false
            };

            SaveFileAndUpdatePicture(picture, pictureFile.InputStream, pictureFile.FileName);

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _pictureRepository.Add(picture);
                unitOfWork.Commit();
            }

            return picture;
        }

        public void Update(Picture picture)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _pictureRepository.Update(picture);
                unitOfWork.Commit();
            }
        }

        public void Remove(Picture picture)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _pictureRepository.Delete(picture);
                unitOfWork.Commit();
            }
        }

        public IList<Picture> Get(int page)
        {
            return _pictureRepository.GetAll(new GenericFilter<Picture>
            {
                Sorting = new Sorting<Picture>
                {
                    OrderByFilter = q => q.OrderBy(c => c.Name)
                },
                Take = AppSettings.PageSize,
                Skip = AppSettings.PageSize*Math.Max(0, page - 1)
            });
        }

        public Picture GetById(int id)
        {
            EnsureValidId(id, "id");
            return _pictureRepository.GetById(id);
        }

        public IList<Picture> GetByIds(params int[] ids)
        {
            var identifiers = ids.ToList();

            return _pictureRepository.GetAll(new GenericFilter<Picture>(p => identifiers.Contains(p.Id))
            {
                Sorting = new Sorting<Picture>
                {
                    OrderByFilter = q => q.OrderBy(c => c.Name)
                }
            });
        }

        private void ValidateFileType(string fileName)
        {
            if (!new Regex("\\.(?i:bmp|gif|jpe?g|png)$").Match(fileName).Success)
                throw new LocalizedValidationException(string.Format(Localization.Picture_TypeNotSupported_Template, Localization.Picture_SupportedTypes_Image));
        }

        private void SaveFileAndUpdatePicture(Picture picture, Stream stream, string fileName, DateTime? replaceDate = null, bool checkNameUniqness = true)
        {
            var file = SaveFile(picture.CategoryId, stream, fileName);
            var thumbnail = CreateThumbnail(file);

            picture.File = file;
            picture.Thumbnail = thumbnail;
            if (checkNameUniqness)
            {
                picture.Name = _pictureRepository.GetUniqueName(picture.CategoryId, picture.Name);
            }

            using (var fileStream = File.OpenRead(_fileStorage.GetFilePhysicalPath(picture.File)))
            using (var image = Image.FromStream(fileStream))
            {
                picture.Width = image.Width;
                picture.Height = image.Height;
            }
        }

        private StoredFile SaveFile(int categoryId, Stream fileStream, string fileName)
        {
            var folder = DefaultDirectories.Category + "\\" + categoryId;

            var tempFolder = _fileStorage.GetFilePhysicalPath(folder);
            FileStorage.EnsureDirectoryExists(tempFolder);
            return _fileStorage.SaveFile(fileStream, fileName, folder, true);
        }

        private StoredFile CreateThumbnail(StoredFile originalFile)
        {
            using (var fileStream = File.OpenRead(_fileStorage.GetFilePhysicalPath(originalFile)))
            using (var originalImage = Image.FromStream(fileStream))
            {
                var percent = GetScale(160, 160, originalImage.Width, originalImage.Height);

                StoredFile thumbnailFile;
                if (percent >= 1.0d)
                {
                    thumbnailFile = originalFile;
                }
                else
                {
                    using (var thumbnail = Resize(originalImage, percent))
                    using (var stream = new MemoryStream())
                    {
                        // Note: use original file format because of MS bug 
                        thumbnail.Save(stream, originalImage.RawFormat);

                        stream.Seek(0, SeekOrigin.Begin);
                        thumbnailFile = _fileStorage.SaveFile(stream, "thumb_" + originalFile.FileName, originalFile.Folder);
                    }
                }
                return thumbnailFile;
            }
        }

        private static double GetScale(int width, int height, int originalWidth, int originalHeight)
        {
            var percentWidth = originalWidth == 0 || width == 0 ? 1.0 : (double)width / originalWidth;
            var percentHeight = originalHeight == 0 || height == 0 ? 1.0 : (double)height / originalHeight;
            var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
            return percent;
        }

        private static Image Resize(Image source, double percent)
        {
            var newWidth = (int)(source.Width * percent);
            var newHeight = (int)(source.Height * percent);

            return source.GetThumbnailImage(newWidth, newHeight, () => false, IntPtr.Zero);
        }
    }
}
