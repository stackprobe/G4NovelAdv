﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		// <---- prm

		public static Game I;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			for (; ; )
			{
				DDCurtain.DrawCurtain();

				//DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 150, 330); // 右下
				DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 70, 330); // 下

				DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 40, 305);

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
