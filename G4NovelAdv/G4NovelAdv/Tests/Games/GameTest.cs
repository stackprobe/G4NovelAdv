using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;
using Charlotte.Scenarios;

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
				Game.I.Scenario = new Scenario("101_スタートシナリオ");
				Game.I.Perform();
			}
		}
	}
}
