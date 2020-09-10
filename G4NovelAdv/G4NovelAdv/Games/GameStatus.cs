using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Charas;
using Charlotte.Games.Walls;

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
		public Wall Wall = new Wall_Dark(); // 軽量な仮設オブジェクト
		public List<Chara> Charas = new List<Chara>(); // 軽量な仮設オブジェクト

		// <---- prm
	}
}
