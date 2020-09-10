using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;
using DxLibDLL;
using Charlotte.Games.Commands;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		private GameStatus Status;

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

		startCurrPage:
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
					this.CurrPage.CharacterName.Length < dispSubtitleChrCount &&
					this.CurrPage.Text.Length < dispChrCount
					)
					dispPageEndedCount++;

				int optionSelectChangeIndex = 0;
				bool optionSelectedFlag = false;
				bool nextPageFlag = false;

				// ? skip Mode
				if (
					1 <= DDInput.R.GetInput() ||
					1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL)
					)
				{
					if (1 <= dispPageEndedCount)
					{
						if (optionSelect == null)
							nextPageFlag = true;
					}
					else
					{
						fastMessageFlag = true;
					}
				}
				else if (DDMouse.Rot < 0)
				{
					if (NEXT_PAGE_KEY_INTERVAL <= dispPageEndedCount)
					{
						if (optionSelect == null)
							nextPageFlag = true;
					}
					else
					{
						fastMessageFlag = true;
					}
					DDEngine.FreezeInput(SHITA_KORO_SLEEP);
				}
				else if (
					DDMouse.L.GetInput() == 1 ||
					//DDKey.GetInput(DX.KEY_INPUT_Z) == 1 || // --> DDInput.A
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1 ||
					DDInput.A.GetInput() == 1
					)
				{
					if (NEXT_PAGE_KEY_INTERVAL <= dispPageEndedCount)
					{
						if (optionSelect != null)
							optionSelectedFlag = true;
						else
							nextPageFlag = true;
					}
					else
					{
						fastMessageFlag = true;
					}
				}
				else if (DDInput.DIR_8.GetInput() == 1)
				{
					if (optionSelect != null)
						optionSelectChangeIndex = -1;
				}
				else if (DDInput.DIR_2.GetInput() == 1)
				{
					if (optionSelect != null)
						optionSelectChangeIndex = 1;
				}

				if (optionSelectChangeIndex != 0)
				{
					int selIdx;

					for (selIdx = 0; selIdx < optionSelect.Items.Count; selIdx++)
						if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), optionSelect.Items[selIdx].GetD4Rect()) == false)
							break;

					if (selIdx == optionSelect.Items.Count)
						selIdx = 0;
					else
						selIdx += optionSelectChangeIndex;

					selIdx %= optionSelect.Items.Count;
					selIdx += optionSelect.Items.Count;
					selIdx %= optionSelect.Items.Count;

					DDMouse.X = optionSelect.Items[selIdx].L + optionSelect.Items[selIdx].W - 10;
					DDMouse.Y = optionSelect.Items[selIdx].T + optionSelect.Items[selIdx].H - 10;
					DDMouse.ApplyPos();
				}
				if (optionSelectedFlag)
				{
					for (int index = 0; index < optionSelect.Items.Count; index++)
					{
						if (DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), optionSelect.Items[index].GetD4Rect()) == false)
						{
							this.Scenario = new Scenario(optionSelect.Items[index].ScenarioName); // 次のシナリオをロード
							this.CurrPageIndex = 0;

							goto startCurrPage;
						}
					}
				}
				if (nextPageFlag)
				{
					this.CurrPageIndex++;

					if (this.Scenario.Pages.Count <= this.CurrPageIndex)
						break;

					goto startCurrPage;
				}

				if (
					//DDKey.GetInput(DX.KEY_INPUT_LEFT) == 1 || // --> DDInput.DIR_4
					DDInput.DIR_4.GetInput() == 1 ||
					0 < DDMouse.Rot
					)
				{
					this.BackLog();
				}

				this.SceneCommonEachFrame();

				//DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 150, 330); // 右下
				DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 70, 330); // 下

				//DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 40, 305); // 左上(はみ出し)
				DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 65, 280); // 左上(重ならない)

				if (fastMessageFlag)
				{
					dispSubtitleChrCount += 2;
					dispChrCount += 2;
				}
				else
				{
					if (DDEngine.ProcFrame % 2 == 0)
						dispSubtitleChrCount++;

					if (DDEngine.ProcFrame % 3 == 0)
						dispChrCount++;
				}
				DDUtils.ToRange(ref dispSubtitleChrCount, 0, IntTools.IMAX); // カンスト対策
				DDUtils.ToRange(ref dispChrCount, 0, IntTools.IMAX); // カンスト対策

#if true // フォント使用(Kゴシック)
				{
					int dispCharaNameLength = Math.Min(dispChrCount, this.CurrPage.CharacterName.Length);
					string dispCharaName = this.CurrPage.CharacterName.Substring(0, dispCharaNameLength);

					DDFontUtils.DrawString(120, 320, dispCharaName, DDFontUtils.GetFont("Kゴシック", 16));
				}

				{
					int dispTextLength = Math.Min(dispChrCount, this.CurrPage.Text.Length);
					string dispText = this.CurrPage.Text.Substring(0, dispTextLength);
					string[] dispLines = dispText.Split('\n');

					for (int index = 0; index < dispLines.Length; index++)
					{
						DDFontUtils.DrawString(120, 380 + index * 30, dispLines[index], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}
#else // MSゴシック
				DDPrint.SetBorder(new I3Color(64, 128, 128));
				DDPrint.SetPrint(120, 320);

				for (int chrIndex = 0; chrIndex <= dispCharaNameChrCount && chrIndex < this.CurrPage.CharacterName.Length; chrIndex++)
				{
					char chr = this.CurrPage.CharacterName[chrIndex];

					DDPrint.Print(new string(new char[] { chr }));
				}

				DDPrint.Reset();

				DDPrint.SetBorder(new I3Color(64, 128, 128));
				DDPrint.SetPrint(120, 380, 24);

				for (int chrIndex = 0; chrIndex <= dispChrCount && chrIndex < this.CurrPage.Text.Length; chrIndex++)
				{
					char chr = this.CurrPage.Text[chrIndex];

					if (chr == '\n')
						DDPrint.PrintRet();
					else
						DDPrint.Print(new string(new char[] { chr }));
				}

				DDPrint.Reset();
#endif

				if (1 <= dispPageEndedCount && optionSelect != null)
				{
					DDCurtain.DrawCurtain(-0.5);

					for (int index = 0; index < optionSelect.Items.Count; index++)
					{
						DDDraw.SetAlpha(0.5);
						DDDraw.DrawRect(
							DDGround.GeneralResource.WhiteBox,
							optionSelect.Items[index].L,
							optionSelect.Items[index].T,
							optionSelect.Items[index].W,
							optionSelect.Items[index].H
							);
						DDDraw.Reset();

						DDFontUtils.DrawString(
							optionSelect.Items[index].Text_X,
							optionSelect.Items[index].Text_Y,
							optionSelect.Items[index].Text,
							DDFontUtils.GetFont("Kゴシック", 16)
							);
					}
				}
				DDEngine.EachFrame();
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.SceneCommonEachFrame();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private void SceneCommonEachFrame()
		{
			foreach (GameScene.CharaInfo charaInfo in this.CurrScene.CharaInfos)
			{
				charaInfo.Effect();
			}

			this.DrawWall();
			this.DrawCharas();
		}

		private void DrawWall()
		{
			if (this.CurrScene.Wall == null)
			{
				DDCurtain.DrawCurtain();
			}
			else
			{
				double zx = DDConsts.Screen_W * 1.0 / this.CurrScene.Wall.Get_W();
				double zy = DDConsts.Screen_H * 1.0 / this.CurrScene.Wall.Get_H();

				double z = Math.Max(zx, zy);

				DDDraw.DrawBegin(this.CurrScene.Wall, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.DrawZoom(z);
				DDDraw.DrawEnd();
			}
		}

		private void DrawCharas()
		{
			for (int charaPos = 0; charaPos < GameScene.CHARA_POS_NUM; charaPos++)
			{
				if (this.CurrScene.Charas[charaPos] != null)
				{
					DDDraw.DrawBegin(
						this.CurrScene.Charas[charaPos],
						GameScene.CHARA_X_POSS[charaPos],
						this.CurrScene.Charas[charaPos].Get_H() * 0.4
						);
					DDDraw.DrawZoom(0.8);
					DDDraw.DrawSlide(
						this.CurrScene.CharaInfos[charaPos].Slide.X,
						this.CurrScene.CharaInfos[charaPos].Slide.Y
						);
					DDDraw.DrawEnd();
				}
			}
		}

		private void BackLog()
		{
			List<string> logLines = new List<string>();

			for (int index = 0; index < this.CurrPageIndex; index++)
				foreach (string line in this.Scenario.Pages[index].Lines)
					logLines.Add(line);

			DDEngine.FreezeInput(SHITA_KORO_SLEEP);

			int backIndex = 0;

			for (; ; )
			{
				if (DDInput.DIR_8.IsPound() || 0 < DDMouse.Rot)
				{
					backIndex++;
				}
				if (DDInput.DIR_2.IsPound() || DDMouse.Rot < 0)
				{
					backIndex--;
				}
				if (DDInput.DIR_6.GetInput() == 1)
				{
					backIndex = -1;
				}
				DDUtils.ToRange(ref backIndex, -1, logLines.Count - 1);

				if (backIndex < 0)
				{
					break;
				}

				this.SceneCommonEachFrame();

				DDCurtain.DrawCurtain(-0.5);

				for (int c = 1; c <= 16; c++)
				{
					int i = logLines.Count - backIndex - c;

					if (0 <= i)
					{
						DDFontUtils.DrawString(120, DDConsts.Screen_H - c * 30 - 15, logLines[i], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput(SHITA_KORO_SLEEP);
		}
	}
}
