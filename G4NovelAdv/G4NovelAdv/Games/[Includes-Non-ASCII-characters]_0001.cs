using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public static class GameEffect_跳び
	{
		private const double R = 10.0;

		public static IEnumerable<bool> GetSequence(GameScene.CharaInfo charaInfo)
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				double x = Math.Cos(scene.Rate * Math.PI) * R - R;
				double y = Math.Sin(scene.Rate * Math.PI) * -R;

				charaInfo.Slide = new D2Point(x, y);

				yield return true;
			}
			Ground.I.SE.Poka01.Play(); // zantei
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				double invRate = 1.0 - scene.Rate;

				double x = Math.Cos(invRate * Math.PI) * R - R;
				double y = Math.Sin(invRate * Math.PI) * -R;

				charaInfo.Slide = new D2Point(x, y);

				yield return true;
			}
			Ground.I.SE.Poka01.Play(); // zantei
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				double invRate = 1.0 - scene.Rate;

				double x = Math.Cos(invRate * Math.PI) * R + R;
				double y = Math.Sin(invRate * Math.PI) * -R;

				charaInfo.Slide = new D2Point(x, y);

				yield return true;
			}
			Ground.I.SE.Poka01.Play(); // zantei
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				double x = Math.Cos(scene.Rate * Math.PI) * R + R;
				double y = Math.Sin(scene.Rate * Math.PI) * -R;

				charaInfo.Slide = new D2Point(x, y);

				yield return true;
			}
			Ground.I.SE.Poka01.Play(); // zantei
			charaInfo.Slide = new D2Point();
		}
	}
}
