﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class Act
	{
		/// <summary>
		/// アクションリスト
		/// </summary>
		private List<Func<bool>> InnerActs = new List<Func<bool>>();

		public void Add(Func<bool> innerAct)
		{
			if (innerAct == null)
				throw new ArgumentException("innerAct == null");

			this.InnerActs.Add(innerAct);
		}

		public void Clear()
		{
			this.InnerActs.Clear();
		}

		/// <summary>
		/// <para>このアクションを描画する。</para>
		/// <para>真を返したとき == アクションを描画し、継続する可能性がある。</para>
		/// <para>偽を返したとき == 全てのアクションは終了しているため描画しなかった。本来の描画を実行する必要がある。</para>
		/// </summary>
		/// <returns>アクション継続か</returns>
		public bool Draw()
		{
			while (1 <= this.InnerActs.Count && this.InnerActs[0]() == false)
				this.InnerActs.RemoveAt(0);

			return 1 <= this.InnerActs.Count;
		}
	}
}
