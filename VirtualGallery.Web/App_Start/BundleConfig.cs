using System.Web;
using System.Web.Optimization;

namespace VirtualGallery.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/common")
                .IncludeDirectory("~/Scripts/common", "*.js")
                .IncludeDirectory("~/Scripts/jquery", "*.js")
                .IncludeDirectory("~/Scripts/graphic", "*.js")
                .IncludeDirectory("~/Scripts/graphic/entities", "*.js")
                .Include("~/Scripts/ShoppingCart.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload")
                        .Include(
                            "~/Scripts/fileupload/jquery.iframe-transport.js",
                            "~/Scripts/fileupload/jquery.fileupload.js",
                            "~/Scripts/fileupload/jquery.attachfile.js")
                            );

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").IncludeDirectory("~/Content/bootstrap", "*.css"));
            bundles.Add(new StyleBundle("~/Content/FontAwesome/css/fontAwesomeCss").Include("~/Content/FontAwesome/css/*.css"));
            bundles.Add(new StyleBundle("~/Content/maincss").Include("~/Content/Site.css"));
        }
    }
}