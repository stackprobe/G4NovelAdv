using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Charas;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの現在の状態を保持する。
	/// セーブ・ロード時はこのクラスの内容を保存・再現する。
	/// </summary>
	public class GameStatus
	{
		public Scenario Scenario;
		public int CurrPageIndex = 0;

		public Action A_DrawWall = () => { };
		public List<Chara> Charas = new List<Chara>();

		// <---- prm
	}
}
