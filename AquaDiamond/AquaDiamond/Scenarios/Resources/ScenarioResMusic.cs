using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Game3Common;

namespace Charlotte.Scenarios.Resources
{
	public class ScenarioResMusic
	{
		private static ScenarioResMusic _i = null;

		public static ScenarioResMusic I
		{
			get
			{
				if (_i == null)
					_i = new ScenarioResMusic();

				return _i;
			}
		}

		private ScenarioResMusic()
		{ }

		private const string MUSIC_FILE_PREFIX = "MusMus\\MusMus-BGM-";
		private const string MUSIC_FILE_SUFFIX = ".mp3";

		public DDMusic GetMusic(string name)
		{
			return CResource.GetMusic(MUSIC_FILE_PREFIX + name + MUSIC_FILE_SUFFIX);
		}
	}
}
