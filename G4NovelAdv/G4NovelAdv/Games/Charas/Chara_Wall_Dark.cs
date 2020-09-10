using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	public class Chara_Wall_Dark : Chara
	{
		public Chara_Wall_Dark()
		{
			this.Z = Z_WALL;
		}

		public override void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
