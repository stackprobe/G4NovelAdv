﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class GameTest
	{
		public void Test01()
		{
			using (new Game())
			{
				Game.I.Perform();
			}
		}

		public void Test02()
		{
			using (new Game())
			{
				Game.I.Status.Scenario = new Scenario(GameConsts.FIRST_SCENARIO_NAME);
				Game.I.Perform();
			}
		}
	}
}
