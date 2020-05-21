using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Scenarios;
using Charlotte.Tools;

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

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			this.CurrPageIndex = 0;

		startCurrPage:
			this.CurrPage = this.Scenario.Pages[this.CurrPageIndex];

			DDEngine.FreezeInput();

			for (int frame = 0; ; frame++)
			{
				if (DDMouse.L.GetInput() == -1)
				{
					this.CurrPageIndex++;
					goto startCurrPage;
				}

				DDCurtain.DrawCurtain();

				//DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 150, 330); // 右下
				DDDraw.DrawSimple(Ground.I.Picture.MessageWin, 70, 330); // 下

				//DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 40, 305); // 左上(はみ出し)
				DDDraw.DrawSimple(Ground.I.Picture.MiniMessageWin, 65, 280); // 左上(重ならない)

				int dispCharNameCharCount = frame / 2;
				int dispCharCount = frame / 3;

				DDPrint.SetBorder(new I3Color(64, 128, 128));
				DDPrint.SetPrint(120, 320);

				for (int index = 0; index <= dispCharNameCharCount && index < this.CurrPage.CharacterName.Length; index++)
				{
					char chr = this.CurrPage.CharacterName[index];

					DDPrint.Print(new string(new char[] { chr }));
				}

				DDPrint.Reset();

				{
					int dispTextLen = Math.Min(dispCharCount, this.CurrPage.Text.Length);
					string dispText = this.CurrPage.Text.Substring(0, dispTextLen);
					string[] dispLines = dispText.Split('\n');

					for (int index = 0; index < dispLines.Length; index++)
					{
						DDFontUtils.DrawString(100, 380 + index * 30, dispLines[index], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}
	}
}
