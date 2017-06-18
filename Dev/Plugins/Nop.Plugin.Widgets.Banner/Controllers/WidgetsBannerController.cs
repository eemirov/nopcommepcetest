using System.Web.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Banner.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Banner.Controllers
{
	public class WidgetsBannerController: BasePluginController
	{
		private readonly IWorkContext _workContext;
		private readonly IStoreContext _storeContext;
		private readonly IStoreService _storeService;
		private readonly ISettingService _settingService;
		private readonly ILocalizationService _localizationService;

		public WidgetsBannerController(IWorkContext workContext,
			IStoreContext storeContext, IStoreService storeService, 
			ISettingService settingService, ILocalizationService localizationService)
		{
			this._workContext = workContext;
			this._storeContext = storeContext;
			this._storeService = storeService;
			this._settingService = settingService;
			this._localizationService = localizationService;
		}

		[AdminAuthorize]
		[ChildActionOnly]
		public ActionResult Configure()
		{
			var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
			var settings = _settingService.LoadSetting<BannerSettings>(storeScope);
			var model = new ConfigurationModel()
			{
				BannerText = settings.BannerText,
				IsVisible = settings.IsVisible
			};

			if (storeScope > 0)
			{
				model.BannerText_OverrideForStore = _settingService.SettingExists(settings, x => x.BannerText, storeScope);
				model.IsVisible_OverrideForStore = _settingService.SettingExists(settings, x => x.IsVisible, storeScope);
			}

			return View("~/Plugins/Widgets.Banner/Views/Configure.cshtml", model);
		}


		[HttpPost]
		[AdminAuthorize]
		[ChildActionOnly]
		public ActionResult Configure(ConfigurationModel input)
		{
			var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
			var settings = _settingService.LoadSetting<BannerSettings>(storeScope);

			settings.BannerText = input.BannerText;
			settings.IsVisible = input.IsVisible;

			_settingService.SaveSettingOverridablePerStore(settings, x => x.BannerText, input.BannerText_OverrideForStore, storeScope, false);
			_settingService.SaveSettingOverridablePerStore(settings, x => x.IsVisible, input.IsVisible_OverrideForStore, storeScope, false);

			_settingService.ClearCache();

			SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

			return Configure();
		}

		[ChildActionOnly]
		public ActionResult PublicInfo(string widgetZone, object additionalData = null)
		{
			var settings = _settingService.LoadSetting<BannerSettings>(_storeContext.CurrentStore.Id);
			var model = new PublicInfoModel()
			{
				BannerText = settings.BannerText,
				IsVisible = settings.IsVisible
			};

			return View("~/Plugins/Widgets.Banner/Views/PublicInfo.cshtml", model);
		}
	}
	}
