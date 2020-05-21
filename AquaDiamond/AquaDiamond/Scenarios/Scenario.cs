using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Scenarios
{
	public class Scenario
	{
		public List<ScenarioPage> Pages = new List<ScenarioPage>();

		public Scenario(string file)
		{
			this.Pages.Clear();

			string[] lines = FileTools.TextToLines(JString.ToJString(DDResource.Load(file), true, true, false, true));
			ScenarioPage page = null;

			foreach (string fLine in lines)
			{
				string line = fLine.Trim();

				if (line == "")
					continue;

				if (line[0] == '/')
				{
					this.Pages.Add(page);

					page = new ScenarioPage()
					{
						CharacterName = line.Substring(1)
					};
				}
				else
				{
					if (page != null)
						page.Lines.Add(line);
				}
			}
			this.Pages.Add(page);
			this.Pages.RemoveAll(v => v == null);

			if (this.Pages.Count < 1)
				throw new DDError();
		}
	}
}
