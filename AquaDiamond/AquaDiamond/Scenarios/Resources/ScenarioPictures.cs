using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Scenarios.Resources
{
	public class ScenarioPictures
	{
		private static ScenarioPictures _i = null;

		public static ScenarioPictures I
		{
			get
			{
				if (_i == null)
					_i = new ScenarioPictures();

				return _i;
			}
		}

		public Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>();

		private ScenarioPictures()
		{
			this.Name2Picture.Add("01a", Ground.I.Picture.Chara_01_A);
			this.Name2Picture.Add("04a", Ground.I.Picture.Chara_04_A);

			// TODO
			// TODO
			// TODO
		}
	}
}
