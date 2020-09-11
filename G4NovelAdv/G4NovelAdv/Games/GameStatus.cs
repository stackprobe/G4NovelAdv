using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Games.Surfaces;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの現在の状態を保持する。
	/// セーブ・ロード時にこのクラスの内容を保存・再現する。
	/// </summary>
	public class GameStatus
	{
		public Scenario Scenario = new Scenario(GameConsts.DUMMY_SCENARIO_NAME); // 軽量な仮設オブジェクト
		public int CurrPageIndex = 0;
		public List<Surface> Surfaces = new List<Surface>(); // 軽量な仮設オブジェクト

		// <---- prm

		public Surface GetSurface(string instanceName)
		{
			foreach (Surface surface in this.Surfaces)
				if (surface.InstanceName == instanceName)
					return surface;

			throw new DDError("存在しないインスタンス名：" + instanceName);
		}
	}
}
