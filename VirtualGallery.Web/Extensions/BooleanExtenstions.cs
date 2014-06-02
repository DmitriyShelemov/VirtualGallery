namespace VirtualGallery.Web.Extensions
{
    public static class BooleanExtenstions
    {
        public static string ToJS(this bool val)
        {
            return val.ToString().ToLower();
        }
    }
}