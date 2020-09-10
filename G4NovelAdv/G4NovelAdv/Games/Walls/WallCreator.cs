using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public static class WallCreator
	{
		public static Wall Create(string name)
		{
			Wall wall;

			switch (name)
			{
				case "Dark": wall = new Wall_Dark(); break;

				// 新しい壁をここへ追加..

				default:
					throw new DDError("不明な壁：" + name);
			}
			return wall;
		}
	}
}
