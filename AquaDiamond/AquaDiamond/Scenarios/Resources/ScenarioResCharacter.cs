﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Scenarios.Resources
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

		private Dictionary<string, DDPicture> Name2Picture = DictionaryTools.Create<DDPicture>(); // zantei

		private ScenarioResCharacter()
		{
			this.Name2Picture.Add("01a", Ground.I.Picture.Chara_01_A);
			this.Name2Picture.Add("01d", Ground.I.Picture.Chara_01_D);
			this.Name2Picture.Add("01h", Ground.I.Picture.Chara_01_H);
			this.Name2Picture.Add("04a", Ground.I.Picture.Chara_04_A);
			this.Name2Picture.Add("06a", Ground.I.Picture.Chara_06_A);
			this.Name2Picture.Add("07a", Ground.I.Picture.Chara_07_A);
			this.Name2Picture.Add("13a", Ground.I.Picture.Chara_13_A);
			this.Name2Picture.Add("13b", Ground.I.Picture.Chara_13_B);
			this.Name2Picture.Add("13c", Ground.I.Picture.Chara_13_C);
			this.Name2Picture.Add("13d", Ground.I.Picture.Chara_13_D);
			this.Name2Picture.Add("13e", Ground.I.Picture.Chara_13_E);
			this.Name2Picture.Add("13f", Ground.I.Picture.Chara_13_F);
			this.Name2Picture.Add("13g", Ground.I.Picture.Chara_13_G);
			this.Name2Picture.Add("13h", Ground.I.Picture.Chara_13_H);
		}

		public DDPicture GetPicture(string name)
		{
			return this.Name2Picture[name];
		}
	}
}
