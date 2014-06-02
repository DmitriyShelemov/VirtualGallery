using System.IO;
using System.Web;

namespace VirtualGallery.BusinessLogic.StoredFiles.Interfaces
{
    public interface IFileStorage
	{
		StoredFile GetFile(int fileId);

        string GetFileUrl(StoredFile file);

        string GetFilePhysicalPath(StoredFile file);

        string GetFilePhysicalPath(string relativePhysicalPath);

        StoredFile SaveFile(HttpPostedFileBase file, string path = null, bool generateServerName = true);

        StoredFile SaveFile(Stream file, string name, string path = null, bool generateServerName = false);

        void DeleteFile(StoredFile storedFile);
    }
}