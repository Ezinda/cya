using System.Web;
using System.Web.Optimization;

namespace mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui-i18n.js",
                        "~/Scripts/jqueryuivsbootstrap-fix.js"));

            bundles.Add(new ScriptBundle("~/bundles/cleavejs").Include(
                        "~/Bower_components/cleave.js/dist/cleave.js",
                        "~/Bower_components/cleave.js/dist/addons/cleave-phone.i18n.js"));

            bundles.Add(new ScriptBundle("~/bundles/pivotjs").Include(
                "~/Bower_components/pivottable/dist/pivot.min.js",
                "~/Bower_components/pivottable/dist/pivot.es.min.js",
                "~/Bower_components/pivottable/dist/tips_data.min.js"));

            bundles.Add(new StyleBundle("~/bundles/pivotcss").Include(
                "~/Bower_components/pivottable/dist/pivot.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/numeraljs").Include(
                        "~/Scripts/numeral/numeral.js",
                        "~/Scripts/ezutils/numeraljs-locale-es.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/newgridjs").Include(
                    "~/Scripts/grid/core.js",
                    "~/Scripts/grid/grid.js"));


            bundles.Add(new StyleBundle("~/Content/newgridcss").Include(
                "~/Content/grid/core.css",
                "~/Content/grid/grid.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                        "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/ezutils").Include(
                        "~/Scripts/ezutils/ezhelpers.js",
                        "~/Scripts/ezutils/ez-breadcrumb.js"));

            bundles.Add(new ScriptBundle("~/bundles/ezgridjs").Include(
                        "~/Scripts/ezutils/ezgridjs.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/tooltip.css",
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/presupuesto").Include(
                      "~/Content/presupuesto-datatable.css"));

            bundles.Add(new StyleBundle("~/Content/css/ezgrid").Include(
                        "~/Content/ezutils/ezgrid.css"));

            bundles.Add(new StyleBundle("~/Content/gridcss").Include(
                      "~/Content/grid-0.4.5.min.css"));

            bundles.Add(new StyleBundle("~/Content/searchboxcss").Include(
                     "~/Content/searchbox.css"));

            bundles.Add(new StyleBundle("~/Scripts/dropzone/css/basic").Include(
                      "~/Scripts/dropzone/basic.css"));

            bundles.Add(new StyleBundle("~/Scripts/dropzone/css/standard").Include(
                      "~/Scripts/dropzone/dropzone.css"));
        }
    }
}
