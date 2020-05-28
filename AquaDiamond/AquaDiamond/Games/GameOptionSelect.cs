using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class GameOptionSelect
	{
		public class ItemInfo
		{
			public string Text;
			public string ScenarioName;

			public int L;
			public int T;
			public int W;
			public int H;

			public int Text_X;
			public int Text_Y;

			// <---- prm

			public D4Rect GetD4Rect()
			{
				return new D4Rect(this.L, this.T, this.W, this.H);
			}
		}

		public List<ItemInfo> Items = new List<ItemInfo>();
	}
}
