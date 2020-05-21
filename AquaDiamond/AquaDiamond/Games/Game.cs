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
		public Scenario Scenario = new Scenario(@"Scenario\Test0001.txt");

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
				// zantei
				if (command.Name == "表示")
				{
					// zantei
					this.CurrScene.Characters[0] = ScenarioResPicture.I.GetPicture(command.Arguments[1]);
				}
			}

			DDEngine.FreezeInput();

			for (int frame = 0; ; frame++)
			{
				if (DDMouse.L.GetInput() == -1)
				{
					this.CurrPageIndex++;

					if (this.Scenario.Pages.Count <= this.CurrPageIndex)
						break;

					goto startCurrPage;
				}

				DDCurtain.DrawCurtain();


				// zantei
				if (this.CurrScene.Characters[0] != null)
				{
					DDDraw.DrawBegin(this.CurrScene.Characters[0], 220, 420);
					DDDraw.DrawZoom(0.75);
					DDDraw.DrawEnd();
				}


				//DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 150, 330); // 右下
				DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 70, 330); // 下

				//DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 40, 305); // 左上(はみ出し)
				DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 65, 280); // 左上(重ならない)

				int dispCharaNameChrCount = frame / 2;
				int dispChrCount = frame / 3;

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
