using System;
using System.IO;
using System.Web;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.BusinessLogic.StoredFiles
{
    public class FileStorage : BaseService, IFileStorage
    {
	    private readonly string _fileStorageBaseUrl;
		private readonly string _fileStorageBasePath;

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly IStoredFileRepository _storedFileRepository;

		public FileStorage(IUnitOfWorkFactory unitOfWorkFactory, IStoredFileRepository storedFileRepository)
		{
		    _unitOfWorkFactory = unitOfWorkFactory;
			_fileStorageBaseUrl = AppSettings.FileStorageBaseUrl;
            _fileStorageBasePath = !string.IsNullOrEmpty(AppSettings.FileStorageBasePath) ? AppSettings.FileStorageBasePath : HttpContext.Current.Request.MapPath(_fileStorageBaseUrl);
		    _storedFileRepository = storedFileRepository;
		}

		public StoredFile GetFile(int fileId)
		{
			return _storedFileRepository.GetById(fileId);
		}

        public string GetFileUrl(StoredFile file)
        {
            var virtualPath = file.PhysicalPath.Replace("\\", "/");
            return String.Format("{0}/{1}", _fileStorageBaseUrl.TrimEnd('/'), virtualPath.TrimStart('/'));
        }

        public string GetFilePhysicalPath(StoredFile file)
        {
            return GetFilePhysicalPath(file.PhysicalPath);
        }

        public StoredFile SaveFile(HttpPostedFileBase file, string path = null, bool generateServerName = true)
        {
            EnsureNotNull(file, "file");

            var originalFileName = file.FileName;

            var serverFileName = generateServerName
                                     ? (Guid.NewGuid() + VirtualPathUtility.GetExtension(originalFileName))
                                     : originalFileName;

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var storedFile = SaveFileInternal(file.InputStream, serverFileName, originalFileName, path);
                
                _storedFileRepository.Add(storedFile);
                unitOfWork.Commit();
             
                return storedFile;
            }
        }

        public StoredFile SaveFile(Stream file, string name, string path = null, bool generateServerName = false)
        {
            var serverFileName = generateServerName
                                     ? (Guid.NewGuid() + VirtualPathUtility.GetExtension(name))
                                     : name;

            return SaveFileInternal(file, serverFileName, name, path);
        }

        public void DeleteFile(StoredFile file)
        {
            EnsureNotNull(file, "file");

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                DeleteFileInternal(file);
                _storedFileRepository.Delete(file);
                unitOfWork.Commit();
            }
            
        }

        public static void EnsureDirectoryExists(string physicalPath)
        {
            var physicalFolder = Path.GetDirectoryName(physicalPath);

            if (string.IsNullOrEmpty(physicalFolder) || Directory.Exists(physicalFolder))
            {
                return;
            }

            Directory.CreateDirectory(physicalFolder);
        }

        private string GetFilePhysicalPath(string storagePath, string filePath)
        {
            return String.Format("{0}\\{1}", storagePath.TrimEnd('\\'), filePath.TrimStart('\\'));
        }

        public string GetFilePhysicalPath(string relativePhysicalPath)
        {
            return GetFilePhysicalPath(_fileStorageBasePath, relativePhysicalPath);
        }

        private StoredFile SaveFileInternal(Stream file, string fileName, string originalName, string relativePath = null)
        {
            if (string.IsNullOrEmpty(relativePath) || !relativePath.StartsWith("\\"))
            {
                relativePath = "\\" + relativePath;
            }
            
            var filePath = String.Format("{0}\\{1}",  relativePath.TrimEnd('\\'), fileName);
            var physicalPath = GetFilePhysicalPath(filePath);
            
            EnsureDirectoryExists(physicalPath);

            using (var savedFile = File.Create(physicalPath))
            {
                file.CopyTo(savedFile);
            }

            return new StoredFile
                {
                    OriginalFileName = originalName,
                    PhysicalPath = filePath, 
                    CreateDate = DateTime.UtcNow,
                    SizeInBytes = file.Length
                };
        }

        public static void DeleteFile(string physicalPath)
        {
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }

        private void DeleteFileInternal(StoredFile file)
        {
            string physicalPath = GetFilePhysicalPath(file);
            DeleteFile(physicalPath);
        }
    }
}