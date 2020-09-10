using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	public static class CharaCreator
	{
		public static Chara Create(string name, int x, int y, int mode = Chara.MODE_DEFAULT)
		{
			Chara chara;

			// HACK: name はクラス名 Chara_<name> と対応する。リフレクションでも良い。

			switch (name)
			{
				case "UFOYukari": chara = new Chara_UFOYukari(); break;

				// 新しいキャラクタをここへ追加..

				default:
					throw new DDError("不明なキャラクタ：" + name);
			}
			chara.Name = name;
			chara.X = x;
			chara.Y = y;
			chara.Mode = mode;

			return chara;
		}
	}
}
