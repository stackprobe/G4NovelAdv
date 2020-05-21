using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Scenarios.Resources
{
	/// <summary>
	/// ここ -> Ground.I.ScenarioResPicture
	/// </summary>
	public class ScenarioResPicture
	{
		private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // zantei

		public ScenarioResPicture()
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
