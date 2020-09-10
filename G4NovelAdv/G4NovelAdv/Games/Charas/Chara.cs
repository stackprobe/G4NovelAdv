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
		public int Mode = MODE_DEFAULT; // 各キャラクタで任意に使用する。

		// <---- prm

		public const int MODE_DEFAULT = 0;

		public abstract void Draw();
	}
}
