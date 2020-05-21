using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Scenarios.Resources
{
	public class ScenarioResPicture
	{
		private static ScenarioResPicture _i = null;

		public static ScenarioResPicture I
		{
			get
			{
				if (_i == null)
					_i = new ScenarioResPicture();

				return _i;
			}
		}

		private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // zantei

		private ScenarioResPicture()
		{
			this.Name2Picture.Add("01a", Ground.I.Picture.Chara_01_A);
			this.Name2Picture.Add("04a", Ground.I.Picture.Chara_04_A);

			// TODO
			// TODO
			// TODO
		}

		public DDPicture GetPicture(string name)
		{
			return this.Name2Picture[name];
		}
	}
}
