namespace VirtualGallery.BusinessLogic.Pictures.Interfaces
{
    public interface IPictureRepository : IBaseRepository<Picture, int>
    {
        string GetUniqueName(int subjectId, string name, string currentName = null);
    }
}