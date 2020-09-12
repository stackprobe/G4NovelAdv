using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Surfaces
{
	public class Surface_Dark : Surface
	{
		protected override void Draw_02()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
