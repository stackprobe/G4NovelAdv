using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Scenarios;
using Charlotte.Tools;
using Charlotte.Scenarios.Resources;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public Scenario Scenario = new Scenario(@"Scenario\Test0001.txt"); // lite dummy object

		// <---- prm

		public static Game I;

		public Game()
		{
			I = this;

			DDUtils.SetMouseDispMode(true);
		}

		public void Dispose()
		{
			DDUtils.SetMouseDispMode(false);

			I = null;
		}

		private int CurrPageIndex;
		private ScenarioPage CurrPage;

		private GameScene CurrScene = new GameScene();

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			this.CurrPageIndex = 0;

		startCurrPage:
			this.CurrPage = this.Scenario.Pages[this.CurrPageIndex];

			foreach (ScenarioCommand command in this.CurrPage.Commands)
			{
				if (command.Name == ScenarioCommand.NAME_表示)
				{
					int charaPos = int.Parse(command.Arguments[0]);
					string charaName = command.Arguments[1];

					if (charaName == ScenarioCommand.ARGUMENT_NONE)
					{
						this.CurrScene.CharaNames[charaPos] = null;
						this.CurrScene.Charas[charaPos] = null;
					}
					else
					{
						this.CurrScene.CharaNames[charaPos] = charaName;
						this.CurrScene.Charas[charaPos] = ScenarioResCharacter.I.GetPicture(charaName);
					}
				}
				else if (command.Name == ScenarioCommand.NAME_背景)
				{
					string wallName = command.Arguments[0];

					if (wallName == ScenarioCommand.ARGUMENT_NONE)
					{
						this.CurrScene.WallName = null;
						this.CurrScene.Wall = null;
					}
					else
					{
						this.CurrScene.WallName = wallName;
						this.CurrScene.Wall = ScenarioResWall.I.GetPicture(wallName);
					}
				}
				else if (command.Name == ScenarioCommand.NAME_揺れ)
				{
					int charaPos = int.Parse(command.Arguments[0]);

					this.CurrScene.CharaInfos[charaPos].Effect = EnumerableTools.Supplier(GameEffect_揺れ.GetSequence(this.CurrScene.CharaInfos[charaPos]));
				}
				else if (command.Name == ScenarioCommand.NAME_跳び)
				{
					int charaPos = int.Parse(command.Arguments[0]);

					this.CurrScene.CharaInfos[charaPos].Effect = EnumerableTools.Supplier(GameEffect_跳び.GetSequence(this.CurrScene.CharaInfos[charaPos]));
				}
			}

			DDEngine.FreezeInput();

			int dispCharaNameChrCount = 0;
			int dispChrCount = 0;
			int dispPageEndedCount = 0;

			bool fastMessageFlag = false;

			for (; ; )
			{
				if (
					this.CurrPage.CharacterName.Length < dispCharaNameChrCount &&
					this.CurrPage.Text.Length < dispChrCount
					)
					dispPageEndedCount++;

				if (DDMouse.L.GetInput() == 1)
				{
					if (10 < dispPageEndedCount)
					{
						this.CurrPageIndex++;

						if (this.Scenario.Pages.Count <= this.CurrPageIndex)
							break;

						goto startCurrPage;
					}
					else
					{
						fastMessageFlag = true;
					}
				}

				foreach (GameScene.CharaInfo charaInfo in this.CurrScene.CharaInfos)
				{
					charaInfo.Effect();
				}

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

				//DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 150, 330); // 右下
				DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 70, 330); // 下

				//DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 40, 305); // 左上(はみ出し)
				DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 65, 280); // 左上(重ならない)

				if (fastMessageFlag)
				{
					dispCharaNameChrCount += 2;
					dispChrCount += 2;
				}
				else
				{
					if (DDEngine.ProcFrame % 2 == 0)
						dispCharaNameChrCount++;

					if (DDEngine.ProcFrame % 3 == 0)
						dispChrCount++;
				}
				DDUtils.Range(ref dispCharaNameChrCount, 0, IntTools.IMAX); // カンスト対策
				DDUtils.Range(ref dispChrCount, 0, IntTools.IMAX); // カンスト対策

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

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
