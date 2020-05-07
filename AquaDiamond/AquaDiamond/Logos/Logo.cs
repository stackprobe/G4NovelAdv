using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Logos
{
	public class Logo : IDisposable
	{
		// <---- prm

		public static Logo I;

		public Logo()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDCurtain.DrawCurtain(scene.Rate - 1.0);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDCurtain.DrawCurtain(-scene.Rate);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
			}
		}
	}
}
