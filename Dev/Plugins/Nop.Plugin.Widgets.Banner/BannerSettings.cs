using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.Banner
{
	public class BannerSettings: ISettings
	{
		public string BannerText { get; set; }
		public bool IsVisible { get; set; }
	}
}
