﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Commands
{
	public static class CommandCreater
	{
		public static Command Create(string name, string[] arguments)
		{
			Command command;

			switch (name)
			{
				case "登場": command = new Command_登場(); break;

				// 新しいコマンドをここへ追加..

				default:
					throw new DDError("不明なコマンド：" + name);
			}
			command.Name = name;
			command.Arguments = arguments;

			command.Loaded();

			return command;
		}
	}
}
