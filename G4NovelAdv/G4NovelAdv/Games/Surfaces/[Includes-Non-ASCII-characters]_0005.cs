using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	public class Surface_背景 : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.背景_森の中, // 800 x 450
			Ground.I.Picture.背景_屋上, // 776 x 900
			Ground.I.Picture.背景_夜, // 800 x 1028 // 余白除く_LTRB == 0, 60, 800, 976
		};

		private int ImageIndex = 0; // 0 ～ (Images.Length - 1)
		private double Zoom = 1.0;

		public override void Draw()
		{
			DDPicture image = this.Images[this.ImageIndex];

			DDDraw.DrawBegin(image, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
			DDDraw.DrawZoom(this.Zoom);
			DDDraw.DrawEnd();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == ScenarioWords.COMMAND_画像)
			{
				int index = int.Parse(arguments[c++]);

				if (index < 0 || this.Images.Length <= index)
					throw new DDError("Bad index: " + index);

				this.ImageIndex = index;
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string Serialize_02()
		{
			return new AttachString().Untokenize(new string[]
			{
				this.ImageIndex.ToString(),
				this.Zoom.ToString("F9"),
			});
		}

		protected override void Deserialize_02(string src)
		{
			string[] lines = new AttachString().Tokenize(src);
			int c = 0;

			this.ImageIndex = int.Parse(lines[c++]);
			this.Zoom = double.Parse(lines[c++]);
		}
	}
}
