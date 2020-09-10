using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Walls
{
	/// <summary>
	/// <para>現在表示中の壁(背景)の状態を保持する。</para>
	/// <para>GameStatus の一部であるため、セーブ・ロード時にこのクラスの内容を保存・再現する。</para>
	/// </summary>
	public abstract class Wall
	{
		/// <summary>
		/// <para>null == 無効</para>
		/// <para>null != アクション中, Draw の代わりにこれを実行する。</para>
		/// <para>ret: ? アクション継続</para>
		/// <para>セーブするとき、この値は保存しない。</para>
		/// </summary>
		public Func<bool> A_Act = null;

		public string Name = "Name_未指定"; // セーブ・ロード用
		public string[] Arguments = DEFAULT_ARGUMENTS; // セーブ・ロード用

		// <---- prm

		public static readonly string[] DEFAULT_ARGUMENTS = new string[0];

		/// <summary>
		/// <para>シナリオの当該ページに到達またはロード時にオブジェクトが生成されたら呼び出される。</para>
		/// <para>Arguments のチェックと準備を行う。</para>
		/// </summary>
		public abstract void Loaded();

		/// <summary>
		/// 壁(背景)を描画する。
		/// </summary>
		public abstract void Draw();
	}
}
