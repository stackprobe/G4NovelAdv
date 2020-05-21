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
					page = new ScenarioPage()
					{
						CharacterName = line.Substring(1)
					};

					this.Pages.Add(page);
				}
				else if (page == null)
				{
					throw new DDError("シナリオの先頭は /xxx でなければなりません。");
				}
				else if (line[0] == '!')
				{
					string[] tokens = line.Substring(1).Split(' ').Where(v => v != "").ToArray();

					page.Commands.Add(new ScenarioCommand()
					{
						Name = tokens[0],
						Arguments = new List<string>(tokens.Skip(1).ToArray()),
					});
				}
				else
				{
					page.Lines.Add(line);
				}
			}
		}
	}
}
