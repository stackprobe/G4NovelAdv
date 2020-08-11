using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests;
using Charlotte.Tests.Games;
using Charlotte.Logos;
using Charlotte.TitleMenus;
using Charlotte.Tests.Logos;
using Charlotte.Games;
using Charlotte.Scenarios;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			DDAdditionalEvents.Ground_INIT = () =>
			{
				//DDGround.RO_MouseDispMode = true;

				// DDInput のキー設定を全て無効にする。
				{
					DDInput.DIR_2.KeyId = -1;
					DDInput.DIR_4.KeyId = -1;
					DDInput.DIR_6.KeyId = -1;
					DDInput.DIR_8.KeyId = -1;
					DDInput.A.KeyId = -1;
					DDInput.B.KeyId = -1;
					DDInput.C.KeyId = -1;
					DDInput.D.KeyId = -1;
					DDInput.E.KeyId = -1;
					DDInput.F.KeyId = -1;
					DDInput.L.KeyId = -1;
					DDInput.R.KeyId = -1;
					DDInput.PAUSE.KeyId = -1;
					DDInput.START.KeyId = -1;
				}
			};

			DDAdditionalEvents.PostGameStart = () =>
			{
				// Font >

				//DDFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
				DDFontRegister.Add(@"Etoile\AquaDiamond\Font\K Gothic\K Gothic.ttf");

				// < Font

				Ground.I = new Ground();
			};

			DDAdditionalEvents.Save = lines =>
			{
				//lines.Add("AquaDiamond-Dummy"); // Dummy

				lines.Add("" + Ground.I.MessageSpeed);

				// 新しい項目をここへ追加...
			};

			DDAdditionalEvents.Load = lines =>
			{
				int c = 0;

				//DDUtils.Noop(lines[c++]); // Dummy

				Ground.I.MessageSpeed = IntTools.ToInt(lines[c++], Consts.MESSAGE_SPEED_MIN, Consts.MESSAGE_SPEED_MAX, Consts.MESSAGE_SPEED_DEF);

				// 新しい項目をここへ追加...
			};

			DDMain2.Perform(Main4);
		}

		private void Main4()
		{
			if (ProcMain.ArgsReader.ArgIs("/D"))
			{
				Main4_Debug();
			}
			else
			{
				Main4_Release();
			}
		}

		private void Main4_Debug()
		{
			Main4_Release();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new GameTest().Test01();
			//new GameTest().Test02();
			//new LogoTest().Test01();
		}

		private void Main4_Release()
		{
			using (new Logo())
			{
				Logo.I.Perform();
			}
			using (new TitleMenu())
			{
				TitleMenu.I.Perform();
			}
		}
	}
}
