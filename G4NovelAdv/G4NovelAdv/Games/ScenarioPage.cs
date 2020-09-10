using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Commands;

namespace Charlotte.Games
{
	public class ScenarioPage
	{
		public string Subtitle = "";
		public List<string> Lines = new List<string>();
		public List<Command> Commands = new List<Command>();

		// <---- prm

		public const int LINE_LEN_MAX = 44; // 要調整
	}
}
