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

			switch (name)
			{
				case "UFOYukari": chara = new Chara_UFOYukari(); break;

				// 新しいキャラクタをここへ追加..

				default:
					throw new DDError("不明なキャラクタ：" + name);
			}
			return chara;
		}
	}
}
