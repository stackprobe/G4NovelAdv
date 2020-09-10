using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Games.Commands;
using DxLibDLL;
using Charlotte.Games.Charas;
using Charlotte.Games.Walls;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public GameStatus Status = new GameStatus(); // 軽量な仮設オブジェクト

		// <---- prm

		public static Game I;

		public Game()
		{
			I = this;

			//DDUtils.SetMouseDispMode(true);
		}

		public void Dispose()
		{
			//DDUtils.SetMouseDispMode(false);

			I = null;
		}

		//private const int NEXT_PAGE_KEY_INTERVAL = 10;
		//private const int SHITA_KORO_SLEEP = 5;

		private ScenarioPage CurrPage;

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			//startCurrPage:
			this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];

			foreach (Command command in this.CurrPage.Commands)
				command.Fire();

			int dispSubtitleCharCount = 0;
			int dispCharCount = 0;
			int dispPageEndedCount = 0;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (
					this.CurrPage.Subtitle.Length < dispSubtitleCharCount &&
					this.CurrPage.Text.Length < dispCharCount
					)
					dispPageEndedCount++;

				if (DDEngine.ProcFrame % 2 == 0)
					dispSubtitleCharCount++;

				if (DDEngine.ProcFrame % 3 == 0)
					dispCharCount++;

				DDUtils.ToRange(ref dispSubtitleCharCount, 0, IntTools.IMAX);
				DDUtils.ToRange(ref dispCharCount, 0, IntTools.IMAX);

				// ====
				// 描画ここから
				// ====

				// 壁(背景)
				{
					Wall wall = Game.I.Status.Wall;

					if (wall.A_Act == null)
						wall.Draw();
					else if (wall.A_Act() == false)
						wall.A_Act = null;
				}

				foreach (Chara chara in Game.I.Status.Charas) // キャラクタ(オブジェクト)
				{
					if (chara.A_Act == null)
						chara.Draw();
					else if (chara.A_Act() == false)
						chara.A_Act = null;
				}

				// メッセージ枠
				{
					const int h = 136;

					DDDraw.DrawRect(Ground.I.Picture.MessageFrame_Message, 0, DDConsts.Screen_H - h, DDConsts.Screen_W, h);
				}

				// サブタイトル文字列
				{
					int dispSubtitleLength = Math.Min(dispCharCount, this.CurrPage.Subtitle.Length);
					string dispSubtitle = this.CurrPage.Subtitle.Substring(0, dispSubtitleLength);

					DDFontUtils.DrawString(120, 320, dispSubtitle, DDFontUtils.GetFont("Kゴシック", 16));
				}

				// シナリオのテキスト文字列
				{
					int dispTextLength = Math.Min(dispCharCount, this.CurrPage.Text.Length);
					string dispText = this.CurrPage.Text.Substring(0, dispTextLength);
					string[] dispLines = dispText.Split('\n');

					for (int index = 0; index < dispLines.Length; index++)
					{
						DDFontUtils.DrawString(120, 380 + index * 30, dispLines[index], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}

				DDEngine.EachFrame();
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				// TODO 描画

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
