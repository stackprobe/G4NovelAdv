﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	public class Surface_Smpl0001 : Surface
	{
		private double A = 1.0;

		protected override void Draw_02()
		{
			DDDraw.SetAlpha(this.A);
			DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X, this.Y);
			DDDraw.DrawRotate(DDEngine.ProcFrame / 100.0);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, string[] arguments)
		{
			if (command == ScenarioWords.COMMAND_フェードイン)
			{
				this.Act.Add(EnumerableTools.Supplier(this.フェードイン()));
			}
			else if (command == ScenarioWords.COMMAND_フェードアウト)
			{
				this.Act.Add(EnumerableTools.Supplier(this.フェードアウト())); // 削除前にアクションを追加する。
				Game.I.Status.RemoveSurface(this); // 自分自身を削除
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(90))
			{
				this.A = scene.Rate;
				this.Draw_02();

				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト()
		{
			foreach (DDScene scene in DDSceneUtils.Create(90))
			{
				this.A = 1.0 - scene.Rate;
				this.Draw_02();

				yield return true;
			}
		}
	}
}
