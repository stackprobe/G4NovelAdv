using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	public static class CharaCreator
	{
		public static Chara Create(string typeName, string instanceName, int x, int y, int mode = Chara.MODE_DEFAULT)
		{
			Chara chara;

			// HACK: typeName はクラス名 Chara_<typeName> と対応する。リフレクションでも良い。-> 難読化するので不可

			switch (typeName)
			{
				case "UFOYukari": chara = new Chara_UFOYukari(); break;

				// 新しいキャラクタをここへ追加..

				default:
					throw new DDError("不明なタイプ名：" + typeName);
			}
			chara.TypeName = typeName;
			chara.InstanceName = instanceName;
			chara.X = x;
			chara.Y = y;
			chara.Mode = mode;

			return chara;
		}
	}
}
