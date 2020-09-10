using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	public abstract class Chara
	{
		/// <summary>
		/// <para>null == 無効</para>
		/// <para>null != アクション中, Draw の代わりにこれを実行する。</para>
		/// <para>ret: ? アクション継続</para>
		/// </summary>
		public Func<bool> A_Act = null;

		public int X = DDConsts.Screen_W / 2;
		public int Y = DDConsts.Screen_H / 2;
		public int Z = Z_CHARA;
		public int Mode = 0; // 各キャラクタで任意に使用する。

		// <---- prm

		public const int Z_WALL = 0;
		public const int Z_CHARA = 1;

		public abstract void Draw();
	}
}
