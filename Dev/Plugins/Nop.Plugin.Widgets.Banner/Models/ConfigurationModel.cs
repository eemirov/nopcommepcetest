using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.Banner.Models
{
	public class ConfigurationModel: BaseNopModel
	{
		public int ActiveStoreScopeConfiguration { get; set; }

		public bool BannerText_OverrideForStore { get; set; }
		[NopResourceDisplayName("Plugins.Widgets.Banner.Text")]
		[AllowHtml]
		public string BannerText { get; set; }

		public bool IsVisible_OverrideForStore { get; set; }
		[NopResourceDisplayName("Plugins.Widgets.Banner.IsVisible")]
		public bool IsVisible { get; set; }
	}
}
