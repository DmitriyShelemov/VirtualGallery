using System.IO;
using Newtonsoft.Json;

namespace VirtualGallery.Web.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string SerializeToString(this JsonSerializer serializer, object data)
        {
            string json;
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, data);
                json = stringWriter.ToString();
            }

            return json;
        }
    }
}