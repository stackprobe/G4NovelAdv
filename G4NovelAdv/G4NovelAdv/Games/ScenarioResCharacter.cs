using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Common.Options;

namespace Charlotte.Games
{
	public class ScenarioResCharacter
	{
		private static ScenarioResCharacter _i = null;

		public static ScenarioResCharacter I
		{
			get
			{
				if (_i == null)
					_i = new ScenarioResCharacter();

				return _i;
			}
		}

		//private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // del @ 2020.5.24

		private ScenarioResCharacter()
		{
		}

		private const string CHARA_FILE_PREFIX = "Etoile\\G4NovelAdv\\わたおきば\\";
		private const string CHARA_FILE_SUFFIX = ".png";

		public DDPicture GetPicture(string name)
		{
			return DDCResource.GetPicture(CHARA_FILE_PREFIX + name + CHARA_FILE_SUFFIX);
		}
	}
}
