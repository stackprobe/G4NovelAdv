using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Scenarios
{
	public class Scenario
	{
		private const string SCENARIO_FILE_PREFIX = "Scenario\\";
		private const string SCENARIO_FILE_SUFFIX = ".txt";

		public List<ScenarioPage> Pages = new List<ScenarioPage>();

		public Scenario(string name)
		{
			string file = SCENARIO_FILE_PREFIX + name + SCENARIO_FILE_SUFFIX;

			this.Pages.Clear();

#if false // 本番用
			byte[] fileData = DDResource.Load(file);
#else
			byte[] fileData;

			{
				const string DEVENV_SCENARIO_DIR = "シナリオデータ";
				const string DEVENV_SCENARIO_SUFFIX = ".txt";

				if (Directory.Exists(DEVENV_SCENARIO_DIR))
				{
					fileData = File.ReadAllBytes(Path.Combine(DEVENV_SCENARIO_DIR, name + DEVENV_SCENARIO_SUFFIX));
				}
				else
				{
					fileData = DDResource.Load(file);
				}
			}
#endif

			string[] lines = FileTools.TextToLines(JString.ToJString(fileData, true, true, false, true));
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
			this.PostCtor();
		}

		private void PostCtor()
		{
			foreach (ScenarioPage page in this.Pages)
			{
				for (int index = 0; index < page.Lines.Count; index++)
				{
					if (ScenarioPage.LINE_LEN_MAX < page.Lines[index].Length)
					{
						page.Lines.Insert(index + 1, page.Lines[index].Substring(ScenarioPage.LINE_LEN_MAX));
						page.Lines[index] = page.Lines[index].Substring(0, ScenarioPage.LINE_LEN_MAX);
					}
				}
			}
		}
	}
}
