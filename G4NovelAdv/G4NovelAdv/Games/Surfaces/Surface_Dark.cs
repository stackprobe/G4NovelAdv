﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Surfaces
{
	public class Surface_Dark : Surface
	{
		public override void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
