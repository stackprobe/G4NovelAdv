using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Commands
{
	public static class CommandCreator
	{
		public static Command Create(string typeName, string[] arguments)
		{
			Command command;

			// HACK: typeName はクラス名 Command_<typeName> と対応する。リフレクションでも良い。-> 難読化するので不可

			switch (typeName)
			{
				case "登場": command = new Command_登場(); break;

				// 新しいコマンドをここへ追加..

				default:
					throw new DDError("不明なタイプ名：" + typeName);
			}
			command.TypeName = typeName;
			command.Arguments = arguments;

			command.Loaded();

			return command;
		}
	}
}
