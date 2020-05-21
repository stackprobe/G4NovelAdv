using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class GameScene
	{
		public const int CHARA_POS_NUM = 5;

		public static readonly int[] CHARA_X_POSS = new int[CHARA_POS_NUM]
		{
			(DDConsts.Screen_W / 8) * 1,
			(DDConsts.Screen_W / 8) * 3,
			DDConsts.Screen_W / 2,
			(DDConsts.Screen_W / 8) * 5,
			(DDConsts.Screen_W / 8) * 7,
		};

		public string[] CharaNames = new string[CHARA_POS_NUM];
		public DDPicture[] Charas = new DDPicture[CHARA_POS_NUM];

		public string WallName = null;
		public DDPicture Wall = null;
	}
}
