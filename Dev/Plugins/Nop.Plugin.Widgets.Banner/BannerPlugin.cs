using System.Web.Routing;
using System.Collections.Generic;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.Banner
{
	public class BannerPlugin: BasePlugin, IWidgetPlugin
	{
		private readonly ISettingService _settingService;


		public BannerPlugin(ISettingService settingService)
		{
			this._settingService = settingService;
		}

		/// <summary>
		/// Gets widget zones where this widget should be rendered
		/// </summary>
		/// <returns>Widget zones</returns>
		public IList<string> GetWidgetZones()
		{
			return new List<string> { "product_details" };
		}

		/// <summary>
		/// Gets a route for provider configuration
		/// </summary>
		/// <param name="actionName">Action name</param>
		/// <param name="controllerName">Controller name</param>
		/// <param name="routeValues">Route values</param>
		public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
		{
			actionName = "Configure";
			controllerName = "WidgetsBanner";
			routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.Banner.Controllers" }, { "area", null } };
		}

		/// <summary>
		/// Gets a route for displaying widget
		/// </summary>
		/// <param name="widgetZone">Widget zone where it's displayed</param>
		/// <param name="actionName">Action name</param>
		/// <param name="controllerName">Controller name</param>
		/// <param name="routeValues">Route values</param>
		public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
		{
			actionName = "PublicInfo";
			controllerName = "WidgetsBanner";
			routeValues = new RouteValueDictionary
			{
				{"Namespaces", "Nop.Plugin.Widgets.Banner.Controllers"},
				{"area", null},
				{"widgetZone", widgetZone}
			};
		}

		public override void Install()
		{
			var settings = new BannerSettings() { BannerText = string.Empty, IsVisible = false};
			_settingService.SaveSetting(settings);

			this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Banner.IsVisible", "Is Visible?");
			this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Banner.Title", "Banner");
			this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Banner.Text", "Text");
			this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Banner.Text.Hint", "Enter text to show it as a banner.");

			base.Install();
		}

		public override void Uninstall()
		{
			_settingService.DeleteSetting<BannerSettings>();

			this.DeletePluginLocaleResource("Plugins.Widgets.Banner.IsVisible");
			this.DeletePluginLocaleResource("Plugins.Widgets.Banner.Title");
			this.DeletePluginLocaleResource("Plugins.Widgets.Banner.Text");
			this.DeletePluginLocaleResource("Plugins.Widgets.Banner.Text.Hint");

			base.Uninstall();
		}
	}
}
