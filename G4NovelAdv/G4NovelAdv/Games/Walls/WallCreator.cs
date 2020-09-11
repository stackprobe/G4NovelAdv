using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public static class WallCreator
	{
		public static Wall Create(string typeName)
		{
			return Create(typeName, Wall.DEFAULT_ARGUMENTS);
		}

		public static Wall Create(string typeName, params string[] arguments)
		{
			Wall wall;

			// HACK: typeName はクラス名 Wall_<typeName> と対応する。リフレクションでも良い。-> 難読化するので不可

			switch (typeName)
			{
				case "Dark": wall = new Wall_Dark(); break;

				// 新しい壁をここへ追加..

				default:
					throw new DDError("不明なタイプ名：" + typeName);
			}
			wall.TypeName = typeName;
			wall.Arguments = arguments;

			wall.Loaded();

			return wall;
		}
	}
}
