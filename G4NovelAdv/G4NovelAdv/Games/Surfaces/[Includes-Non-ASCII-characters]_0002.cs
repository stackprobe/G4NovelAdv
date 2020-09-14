﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// からい氏
	/// 結月ゆかり
	/// やや右向き
	/// </summary>
	public class Surface_結月ゆかり : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.結月ゆかり01, // 0 - コーヒー
			Ground.I.Picture.結月ゆかり02, // 1 - 汗マキ
			Ground.I.Picture.結月ゆかり03, // 2 - 笑マキ
			Ground.I.Picture.結月ゆかり04, // 3 - 魔術
			Ground.I.Picture.結月ゆかり05, // 4 - マキ肉
			Ground.I.Picture.結月ゆかり11, // 5 - 殺
			Ground.I.Picture.結月ゆかり12, // 6 - 寝
			Ground.I.Picture.結月ゆかり13, // 7 - リボン
			Ground.I.Picture.結月ゆかり14, // 8 - 刀
			Ground.I.Picture.結月ゆかり15, // 9 - 塗
			Ground.I.Picture.結月ゆかり16, // 10 - 普通
		};

		private int Mode = MODE_DEFAULT; // 0 - (Images.Length - 1)

		public const int MODE_DEFAULT = 10;

		public override void Draw()
		{
			const double R = 0.0;

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
