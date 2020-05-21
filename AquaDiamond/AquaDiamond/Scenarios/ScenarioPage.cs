using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Scenarios
{
	public class ScenarioPage
	{
		public string CharacterName = "";
		public List<string> Lines = new List<string>();

		public string Text
		{
			get
			{
				return string.Join("\n", this.Lines);
			}
		}
	}
}
