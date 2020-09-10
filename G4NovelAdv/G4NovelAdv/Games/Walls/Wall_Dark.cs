using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public class Wall_Dark : Wall
	{
		public override void Loaded()
		{
			throw new NotImplementedException();
		}

		public override void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
