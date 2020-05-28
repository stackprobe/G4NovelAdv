using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Game3Common;

namespace Charlotte.Scenarios.Resources
{
	public class ScenarioResWall
	{
		private static ScenarioResWall _i = null;

		public static ScenarioResWall I
		{
			get
			{
				if (_i == null)
					_i = new ScenarioResWall();

				return _i;
			}
		}

		//private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // del @ 2020.5.24

		private ScenarioResWall()
		{
#if false // del @ 2020.5.24
			this.Name2Picture.Add("02a", Ground.I.Picture.Wall_02_A);
			this.Name2Picture.Add("04a", Ground.I.Picture.Wall_04_A);
			this.Name2Picture.Add("14a", Ground.I.Picture.Wall_14_A);
			this.Name2Picture.Add("15a", Ground.I.Picture.Wall_15_A);
			this.Name2Picture.Add("16a", Ground.I.Picture.Wall_16_A);
			this.Name2Picture.Add("27a", Ground.I.Picture.Wall_27_A);
			this.Name2Picture.Add("29a", Ground.I.Picture.Wall_29_A);
			this.Name2Picture.Add("39a", Ground.I.Picture.Wall_39_A);
#endif
		}

		private const string WALL_FILE_PREFIX = "きまぐれアフター\\BG";
		private const string WALL_FILE_SUFFIX = "_80.jpg";

		public DDPicture GetPicture(string name)
		{
#if true
			return CResource.GetPicture(WALL_FILE_PREFIX + name + WALL_FILE_SUFFIX);
#else // del @ 2020.5.24
			return this.Name2Picture[name];
#endif
		}
	}
}
