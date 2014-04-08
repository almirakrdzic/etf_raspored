using System.Web;
using System.Web.Optimization;

namespace DigitalLibrary
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.min.js"));

             bundles.Add(new ScriptBundle("~/bundles/piechartjs").Include(
                        "~/Scripts/jquery.easy-pie-chart.js"));

             bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                        "~/Scripts/admin.js"));

             bundles.Add(new ScriptBundle("~/bundles/indexjs").Include(
                        "~/Scripts/index.js"));

             bundles.Add(new ScriptBundle("~/bundles/scriptsjs").Include(
                         "~/Scripts/scripts.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/dmuploaderjs").Include(
                       "~/Scripts/dmuploader.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxjs").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                       "~/Scripts/angular.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/jquery.easy-pie-chart.css",
                        "~/Content/uploader.css",
                        "~/Content/styles.css",
                        "~/Content/bootstrap-responsive.min.css"));
        }
    }
}