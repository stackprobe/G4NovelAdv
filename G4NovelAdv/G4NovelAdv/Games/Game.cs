﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Games.Surfaces;
using DxLibDLL;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public GameStatus Status = new GameStatus(); // 軽量な仮設オブジェクト

		// <---- prm

		public static Game I;

		public DDTaskList SurfaceEL;

		public Game()
		{
			I = this;

			this.SurfaceEL = new DDTaskList();
		}

		public void Dispose()
		{
			this.SurfaceEL = null;

			I = null;
		}

		private const int NEXT_PAGE_KEY_INTERVAL = 10;
		//private const int SHITA_KORO_SLEEP = 5;

		private ScenarioPage CurrPage;

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

		restartCurrPage:
			this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];

			foreach (ScenarioCommand command in this.CurrPage.Commands)
				command.Invoke();

			int dispSubtitleCharCount = 0;
			int dispCharCount = 0;
			int dispPageEndedCount = 0;
			bool dispFastMode = false;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				// 入力：シナリオを進める。
				if (
					DDMouse.L.GetInput() == -1 ||
					DDInput.A.GetInput() == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1
					)
				{
					if (dispPageEndedCount < NEXT_PAGE_KEY_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						dispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						this.Status.CurrPageIndex++;

						if (this.Status.Scenario.Pages.Count <= this.Status.CurrPageIndex)
							break;

						goto restartCurrPage;
					}
				}

				if (
					this.CurrPage.Subtitle.Length < dispSubtitleCharCount &&
					this.CurrPage.Text.Length < dispCharCount
					)
					dispPageEndedCount++;

				if (dispFastMode)
				{
					dispSubtitleCharCount += 2;
					dispCharCount += 2;
				}
				else
				{
					if (DDEngine.ProcFrame % 2 == 0)
						dispSubtitleCharCount++;

					if (DDEngine.ProcFrame % 3 == 0)
						dispCharCount++;
				}
				DDUtils.ToRange(ref dispSubtitleCharCount, 0, IntTools.IMAX);
				DDUtils.ToRange(ref dispCharCount, 0, IntTools.IMAX);

				// ====
				// 描画ここから
				// ====

				this.DrawSurfaces();

				// メッセージ枠
				{
					const int h = 136;

					DDDraw.SetAlpha(0.7);
					DDDraw.DrawRect(Ground.I.Picture.MessageFrame_Message, 0, DDConsts.Screen_H - h, DDConsts.Screen_W, h);
					DDDraw.Reset();
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
						DDFontUtils.DrawString(10, 450 + index * 30, dispLines[index], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}

				DDEngine.EachFrame();
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawSurfaces();

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private void DrawSurfaces()
		{
			DDCurtain.DrawCurtain(); // 画面クリア

			Game.I.Status.Surfaces.Sort((a, b) => a.Z - b.Z); // Z-オーダー順

			foreach (Surface surface in Game.I.Status.Surfaces) // キャラクタ・オブジェクト・壁紙
				if (surface.Act.Draw() == false)
					surface.Draw();

			Game.I.SurfaceEL.ExecuteAllTask(); // キャラクタ・オブジェクト・壁紙 の エフェクト
		}
	}
}
