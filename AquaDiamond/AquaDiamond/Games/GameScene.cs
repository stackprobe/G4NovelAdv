using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

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

		public class CharaInfo
		{
			public D2Point Slide;

			/// <summary>
			/// エフェクト終了時、変更したものをもとに戻すこと。Reset()の実行を期待しないこと。
			/// 戻り値は多分見ていない。
			/// </summary>
			public Func<bool> Effect; // ret: ? エフェクト継続

			public void Reset()
			{
				this.Slide = new D2Point();
				this.Effect = () => false;
			}
		}

		public CharaInfo[] CharaInfos = new CharaInfo[CHARA_POS_NUM];

		public string WallName = null;
		public DDPicture Wall = null;

		public GameScene()
		{
			for (int index = 0; index < CHARA_POS_NUM; index++)
			{
				this.CharaInfos[index] = new CharaInfo();
				this.CharaInfos[index].Reset();
			}
		}
	}
}
