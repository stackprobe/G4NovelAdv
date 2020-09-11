using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Charas
{
	/// <summary>
	/// 現在登場中のキャラクタやオブジェクトの状態を保持する。
	/// GameStatus の一部であるため、セーブ・ロード時にこのクラスの内容を保存・再現する。
	/// </summary>
	public abstract class Chara
	{
		/// <summary>
		/// <para>null == 無効</para>
		/// <para>null != アクション中, Draw の代わりにこれを実行する。</para>
		/// <para>ret: ? アクション継続</para>
		/// <para>セーブするとき、この値は保存しない。</para>
		/// </summary>
		public Func<bool> A_Act = null;

		public string TypeName; // セーブ・ロード用
		public string InstanceName;
		public int X;
		public int Y;
		public int Mode = MODE_DEFAULT; // 各キャラクタで任意に使用する。

		// <---- prm

		public const int MODE_DEFAULT = 0;

		/// <summary>
		/// キャラクタ(オブジェクト)を描画する。
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// 指示する。
		/// </summary>
		/// <param name="name">指示の名前</param>
		/// <param name="arguments">指示の引数</param>
		public abstract void Instruct(string name, params string[] arguments);
	}
}
