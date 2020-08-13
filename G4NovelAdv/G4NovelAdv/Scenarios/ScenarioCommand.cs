using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Scenarios
{
	public class ScenarioCommand
	{
		public const string NAME_登場 = "登場";
		public const string NAME_退場 = "退場";
		public const string NAME_背景 = "背景";
		public const string NAME_音楽 = "音楽";
		public const string NAME_揺れ = "揺れ";
		public const string NAME_跳び = "跳び";
		public const string NAME_分岐 = "分岐";

		public const string ARGUMENT_無し = "無し";

		// ----

		public string Name;
		public List<string> Arguments;
	}
}
