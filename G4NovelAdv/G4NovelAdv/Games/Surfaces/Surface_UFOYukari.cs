﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	public class Surface_UFOYukari : Surface
	{
		private double Real_X;
		private double Real_Y;
		private double Real_Zoom = 0.5;

		public override void Draw()
		{
			double x = this.X;
			double y = this.Y;

			x += Math.Sin(DDEngine.ProcFrame / 71.0) * 60.0;
			y += Math.Sin(DDEngine.ProcFrame / 61.0) * 90.0;

			DDUtils.Approach(ref this.Real_X, x, 0.999);
			DDUtils.Approach(ref this.Real_Y, y, 0.999);

			this.DrawYukari();
		}

		private void DrawYukari()
		{
			this.DrawYukari(this.Real_X, this.Real_Y, this.Real_Zoom);
		}

		private void DrawYukari(double x, double y, double zoom)
		{
#if true
			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_からだ, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 1.3) * 0.03);
			DDDraw.DrawZoom(zoom);
			DDDraw.DrawEnd();

			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_あたま0000, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 0.4) * 0.03);
			DDDraw.DrawZoom(zoom);
			DDDraw.DrawEnd();

			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_UFO, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 0.0) * 0.03);
			DDDraw.DrawZoom(zoom);
			DDDraw.DrawEnd();
#else // old
			Action<DDPicture> draw = picture =>
			{
				DDDraw.DrawBegin(picture, x, y);
				DDDraw.DrawZoom(zoom);
				DDDraw.DrawEnd();
			};

			draw(Ground.I.Picture.未確認飛行ゆかりん_からだ);
			draw(Ground.I.Picture.未確認飛行ゆかりん_あたま0000);
			draw(Ground.I.Picture.未確認飛行ゆかりん_UFO);
#endif
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			if (command == ScenarioWords.COMMAND_跳ねて登場)
			{
				this.Act.Add(EnumerableTools.Supplier(this.跳ねて登場()));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> 跳ねて登場()
		{
			foreach (var info in new[]
			{
				new { f = 60, hi = 100.0 },
				new { f = 40, hi = 50.0 },
				new { f = 30, hi = 25.0 },
			})
			{
				foreach (DDScene scene in DDSceneUtils.Create(info.f))
				{
					double x = this.X;
					double y = this.Y;

					y -= DDUtils.Parabola(scene.Rate) * info.hi;

					this.Real_X = x;
					this.Real_Y = y;

					this.DrawYukari();

					yield return true;
				}
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Real_X.ToString("F9"),
				this.Real_Y.ToString("F9"),
				this.Real_Zoom.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Real_X = double.Parse(lines[c++]);
			this.Real_Y = double.Parse(lines[c++]);
			this.Real_Zoom = double.Parse(lines[c++]);
		}
	}
}
