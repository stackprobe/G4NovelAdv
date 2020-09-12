using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	public class Surface_UFOYukari : Surface
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

		protected override void Invoke_02(string command, string[] arguments)
		{
			if (command == "跳んで登場")
			{
				this.Act.Add(EnumerableTools.Supplier(this.跳んで登場()));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> 跳んで登場()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				// TODO

				yield return true;
			}
		}
	}
}
