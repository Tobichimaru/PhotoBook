using System.Web.Optimization;

namespace MvcPL
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jcrop").Include(
                "~/Scripts/jquery.Jcrop.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                "~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle("~/bundles/blueimp").Include(
                "~/Content/blueimp-gallery2/js/blueimp-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-blueimp").Include(
                "~/Content/blueimp-gallery2/js/jquery.*"));


            bundles.Add(new StyleBundle("~/Content/blueimp").Include(
                "~/Content/blueimp-gallery2/css/blueimp-*"));
            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jcrop").Include(
                "~/Content/jquery.Jcrop.css"));

            
        }
    }
}