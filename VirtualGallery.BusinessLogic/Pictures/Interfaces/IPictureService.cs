using System.Collections.Generic;
using System.Web;

namespace VirtualGallery.BusinessLogic.Pictures.Interfaces
{
    public interface IPictureService
    {
        Picture Add(int categoryId, HttpPostedFileBase picture);

        void Update(Picture picture);

        void Remove(Picture picture);

        IList<Picture> Get(int page);

        Picture GetById(int id);
    }
}
