using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

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

		private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // zantei

		private ScenarioResWall()
		{
			this.Name2Picture.Add("02a", Ground.I.Picture.Wall_02_A);
			this.Name2Picture.Add("04a", Ground.I.Picture.Wall_04_A);
			this.Name2Picture.Add("14a", Ground.I.Picture.Wall_14_A);
			this.Name2Picture.Add("15a", Ground.I.Picture.Wall_15_A);
			this.Name2Picture.Add("16a", Ground.I.Picture.Wall_16_A);
			this.Name2Picture.Add("27a", Ground.I.Picture.Wall_27_A);
			this.Name2Picture.Add("29a", Ground.I.Picture.Wall_29_A);
			this.Name2Picture.Add("39a", Ground.I.Picture.Wall_39_A);
		}

		public DDPicture GetPicture(string name)
		{
			return this.Name2Picture[name];
		}
	}
}
