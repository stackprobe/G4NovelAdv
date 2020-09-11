using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	public class Chara_UFOYukari : Chara
	{
		public override void Draw()
		{
			double x = this.X;
			double y = this.Y;
			double z = 0.5;

			x += Math.Sin(DDEngine.ProcFrame / 101.0) * 5.0;
			y += Math.Sin(DDEngine.ProcFrame / 301.0) * 50.0;

			Action<DDPicture> draw = picture =>
			{
				DDDraw.DrawBegin(picture, x, y);
				DDDraw.DrawZoom(z);
				DDDraw.DrawEnd();
			};

			draw(Ground.I.Picture.未確認飛行ゆかりん_からだ);
			draw(Ground.I.Picture.未確認飛行ゆかりん_あたま0000);
			draw(Ground.I.Picture.未確認飛行ゆかりん_UFO);
		}

		public override void Instruct(string name, params string[] arguments)
		{

		}
	}
}
