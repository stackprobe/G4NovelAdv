using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public static class GameEffect_揺れ
	{
		public static IEnumerable<bool> GetSequence(GameScene.CharaInfo charaInfo)
		{
			Ground.I.SE.Hit01.Play(); // zantei

			double r = 10.0;

			for (int c = 0; c < 30; c++)
			{
				double rad = DDUtils.Random.Real() * Math.PI * 2.0;

				double x = Math.Cos(rad) * r;
				double y = Math.Sin(rad) * r;

				charaInfo.Slide = new D2Point(x, y);

				r *= 0.97;

				yield return true;
			}
			charaInfo.Slide = new D2Point(0, 0);
		}
	}
}
