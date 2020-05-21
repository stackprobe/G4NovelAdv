using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Scenarios
{
	public class ScenarioCommand
	{
		public const string NAME_表示 = "表示";

		public const string ARGUMENT_NONE = "NONE";

		// ----

		public string Name;
		public List<string> Arguments;
	}
}
