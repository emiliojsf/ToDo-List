using System.Web;
using System.Web.Optimization;

namespace FirstAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/Scripts/angular/main.js")
                .Include("~/Scripts/app/app.js")
                .IncludeDirectory("~/Scripts/app/", "*.js", searchSubdirectories: true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            /*bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            */
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/angular-material.min.css")
                      .Include("~/Content/mdPickers.css")
                      .Include("~/Content/mdPickers.min.css")
                      .Include("~/Content/Site.css"));
            

            /*

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/app.js"));
            */
        }
    }
}
