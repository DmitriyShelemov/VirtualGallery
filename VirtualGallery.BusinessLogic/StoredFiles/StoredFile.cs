using System;

namespace VirtualGallery.BusinessLogic.StoredFiles
{
    public class StoredFile
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; }

        public string PhysicalPath { get; set; }

        public DateTime CreateDate { get; set; }

        public long SizeInBytes { get; set; }

        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(PhysicalPath);
            }
        }

        public string Folder
        {
            get
            {
                return System.IO.Path.GetDirectoryName(PhysicalPath);
            }
        }
    }
}