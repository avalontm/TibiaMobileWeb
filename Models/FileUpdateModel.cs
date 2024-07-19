namespace TibiaMobileWeb.Models
{
    public class FileUpdateModel
    {
        public string version { get; set; }
        public string host { get; set; }
        public FileItemModel[] items { get; set; }

        public FileUpdateModel()
        {
        }
    }

    public class FileItemModel
    {
        public string crc32 { get; set; }
        public string file { get; set; }
    }

}
