using System.Web.Optimization;

namespace Ruann.Linde {
	public static class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            // script bundles
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js", "~/Scripts/respond.js"));
            //bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js", "~/Scripts/angular-route.js", "~/Scripts/angular-animate.js", "~/Scripts/ngStorage.js", "~/Scripts/ng-infinate-scroll.js"));
            //bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/Chart.js", "~/Scripts/angular-chart.js"));
            //bundles.Add(new ScriptBundle("~/bundles/file-upload").Include("~/Scripts/FileAPI.min.js", "~/Scripts/ng-file-upload.min.js", "~/Scripts/ng-file-upload-shim.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/rl-open-weather-map.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/rl-geolocator.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/rl-google-api.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/rl-app.js"));
			bundles.Add(new ScriptBundle("~/bundles/intuit").Include("~/Scripts/app/rl-intuit.js"));

            // style bundles
            //bundles.Add(new StyleBundle("~/content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css"));
            //bundles.Add(new StyleBundle("~/content/font-awesome").Include("~/Content/font-awesome.css"));
            //bundles.Add(new StyleBundle("~/content/main").Include("~/Content/Main.css"));
            //bundles.Add(new StyleBundle("~/content/intuit").Include("~/Content/Intuit.css"));
            //bundles.Add(new StyleBundle("~/content/cv").Include("~/Content/CV.css"));
            //bundles.Add(new StyleBundle("~/content/accounting").Include("~/Content/Accounting.css"));

            //BundleTable.EnableOptimizations = true;

        }
    }
}