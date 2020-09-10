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
			return Create(name, Wall.DEFAULT_ARGUMENTS);
		}

		public static Wall Create(string name, string[] arguments)
		{
			Wall wall;

			// HACK: name はクラス名 Wall_<name> と対応する。リフレクションでも良い。

			switch (name)
			{
				case "Dark": wall = new Wall_Dark(); break;

				// 新しい壁をここへ追加..

				default:
					throw new DDError("不明な壁：" + name);
			}
			wall.Name = name;
			wall.Arguments = arguments;

			wall.Loaded();

			return wall;
		}
	}
}
