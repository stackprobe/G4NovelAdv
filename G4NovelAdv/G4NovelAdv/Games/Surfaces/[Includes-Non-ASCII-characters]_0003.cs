﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// からい氏
	/// 弦巻マキ
	/// やや左向き
	/// </summary>
	public class Surface_弦巻マキ : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.弦巻マキ01, // 0 - 制服_目玉焼きパン
			Ground.I.Picture.弦巻マキ02, // 1 - 制服_殺
			Ground.I.Picture.弦巻マキ03, // 2 - 制服_眼鏡
			Ground.I.Picture.弦巻マキ04, // 3 - 普
			Ground.I.Picture.弦巻マキ05, // 4 - 泣
			Ground.I.Picture.弦巻マキ06, // 5 - 喜
		};

		private int Mode = MODE_DEFAULT; // 0 - (Images.Length - 1)

		public const int MODE_DEFAULT = 3;

		public override void Draw()
		{
			const double R = 0.5;

			DDDraw.DrawBegin(this.Images[this.Mode], this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + R) * 2.0);
			DDDraw.DrawZoom(0.5);
			DDDraw.DrawEnd();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == ScenarioWords.COMMAND_Mode)
			{
				int mode = int.Parse(arguments[c++]);

				if (mode < 0 || this.Images.Length <= mode)
					throw new DDError("Bad mode: " + mode);

				this.Mode = mode;
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Mode.ToString(),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Mode = int.Parse(lines[c++]);
		}
	}
}
