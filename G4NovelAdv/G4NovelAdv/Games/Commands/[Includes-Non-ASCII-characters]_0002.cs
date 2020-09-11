using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Games.Charas;

namespace Charlotte.Games.Commands
{
	public class Command_登場 : Command
	{
		private string InstanceName;
		private string CharaTypeName;
		private int X;
		private int Y;
		private int Mode;

		public override void Loaded()
		{
			if (this.Arguments.Length == 5)
			{
				int c = 0;

				this.InstanceName = this.Arguments[c++];
				this.CharaTypeName = this.Arguments[c++];
				this.X = IntTools.ToInt(this.Arguments[c++], 0, DDConsts.Screen_W, DDConsts.Screen_W / 2);
				this.Y = IntTools.ToInt(this.Arguments[c++], 0, DDConsts.Screen_H, DDConsts.Screen_H / 2);
				this.Mode = IntTools.ToInt(this.Arguments[c++], 0, IntTools.IMAX, Chara.MODE_DEFAULT);
			}
			else
			{
				throw new DDError("不正なキャラクタ引数");
			}
		}

		public override void Fire()
		{
			Game.I.Status.Charas.Add(CharaCreator.Create(this.CharaTypeName, this.InstanceName, this.X, this.Y, this.Mode)); // 引数の並びに注意 typeName, instanceName
		}
	}
}
