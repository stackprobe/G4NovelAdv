using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Commands
{
	public abstract class Command
	{
		public string Name;
		public string[] Arguments;

		// <---- prm

		/// <summary>
		/// <para>シナリオ読み込み時に呼び出される。</para>
		/// <para>Arguments のチェックと、コマンド実行の準備を行う。</para>
		/// </summary>
		public abstract void Loaded();

		/// <summary>
		/// <para>コマンド実行</para>
		/// <para>シナリオの当該ページに到達したら呼び出される。</para>
		/// <para>呼び出されるのは一度だけ。</para>
		/// </summary>
		public abstract void Fire();
	}
}
