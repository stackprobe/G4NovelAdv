using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

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
		private double Zoom;

		public override void Draw()
		{
			DDPicture image = this.Images[this.ImageIndex];

			DDDraw.DrawBegin(image, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
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
			return this.ImageIndex.ToString();
		}

		protected override void Deserialize_02(string value)
		{
			this.ImageIndex = int.Parse(value);
		}
	}
}
