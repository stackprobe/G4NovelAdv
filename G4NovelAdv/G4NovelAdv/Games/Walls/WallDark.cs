﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public class WallDark : Wall
	{
		public override void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
