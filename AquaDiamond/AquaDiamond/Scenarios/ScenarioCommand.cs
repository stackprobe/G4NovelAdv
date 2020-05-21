using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Scenarios
{
	public class ScenarioCommand
	{
		public const string NAME_表示 = "表示";
		public const string NAME_背景 = "背景";
		public const string NAME_揺れ = "揺れ";
		public const string NAME_跳び = "跳び";
		public const string NAME_分岐 = "分岐";

		public const string ARGUMENT_NONE = "NONE";

		// ----

		public string Name;
		public List<string> Arguments;
	}
}
