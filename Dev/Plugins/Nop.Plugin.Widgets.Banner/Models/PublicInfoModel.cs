using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.Banner.Models
{
	public class PublicInfoModel: BaseNopModel
	{
		public string BannerText { get; set; }
		public bool IsVisible { get; set; }
	}
}
