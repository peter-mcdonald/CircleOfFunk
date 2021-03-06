﻿using System.Web.Optimization;

namespace CircleOfFunk
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/site")
                            .Include("~/Scripts/jquery-{version}.js")
                            .Include("~/Scripts/jquery.easing.1.3.js")
                            .Include("~/Scripts/liteaccordion.jquery.js")
                            .Include("~/Scripts/spin.js")
                            .Include("~/Scripts/jquery.easy-ticker.js")
                            .Include("~/Scripts/famax-fix.js")
                            .Include("~/Scripts/soundcloud.js")
                            .Include("~/Scripts/localStorage.js")
                            .Include("~/Scripts/registration.js")
                            .Include("~/Scripts/parseUri-1.2.2.js")
                            .Include("~/Scripts/jquery.simplemodal-1.4.4.js")
                            .Include("~/Scripts/history.js/history.js")
                            .Include("~/Scripts/history.js/history.adapter.jquery.js")
                            .Include("~/Scripts/jquery.unobtrusive*","~/Scripts/jquery.validate*")
                            .Include("~/Scripts/CircleOfFunk.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css")
                   .Include("~/Content/site.css")
                   .Include("~/Content/liteaccordion.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));


#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}