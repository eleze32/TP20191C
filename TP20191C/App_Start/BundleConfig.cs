using System.Web;
using System.Web.Optimization;

namespace TP20191C
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/Jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/JqueryValidate/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/Bootstrap/bootstrap.js",
                      "~/Content/Scripts/Respond/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/data-table").Include(
                "~/Content/Scripts/DataTable/jquery.dataTables.min.js",
                "~/Content/Scripts/DataTable/dataTables.bootstrap4.min.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/admin-pregutas").Include(
                "~/Content/Scripts/admin-preguntas/admin-preguntas.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Css/Bootstrap/bootstrap.css",
                      "~/Content/Css/Fontawesome/all.css",
                      "~/Content/Css/app.css",
                      "~/Content/Css/DataTable/dataTables.bootstrap4.min.css"));
        }
    }
}
