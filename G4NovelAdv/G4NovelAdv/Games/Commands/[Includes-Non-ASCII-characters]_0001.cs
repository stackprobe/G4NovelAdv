using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Commands
{
	public class Command_指示 : Command
	{
		private string InstanceName;
		private string InstructCommand;
		private string[] InstructArguments;

		public override void Loaded()
		{
			this.InstanceName = this.Arguments[0];
			this.InstructCommand = this.Arguments[1];
			this.InstructArguments = this.Arguments.Skip(2).ToArray();
		}

		public override void Fire()
		{
			Game.I.Status.GetChara(this.InstanceName).Instruct(this.InstructCommand, this.InstructArguments);
		}
	}
}
