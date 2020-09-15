﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	/// <summary>
	/// シナリオのコマンドで使う定数文字列
	/// </summary>
	public static class ScenarioWords
	{
		// -- 共通 - Surface.Invoke 内で処理

		public const string COMMAND_移動 = "移動";
		public const string COMMAND_X = "X";
		public const string COMMAND_Y = "Y";
		public const string COMMAND_Z = "Z";
		public const string COMMAND_End = "End";

		// -- 個別 - Surface_*.Invoke_02 内で処理

		public const string COMMAND_Chara = "Chara";
		public const string COMMAND_Mode = "Mode";

		public const string COMMAND_フェードイン = "フェードイン";
		public const string COMMAND_フェードアウト = "フェードアウト";

		public const string COMMAND_跳ねて登場 = "跳ねて登場";

		public const string COMMAND_画像 = "画像";
		public const string COMMAND_Slide = "Slide";
	}
}
