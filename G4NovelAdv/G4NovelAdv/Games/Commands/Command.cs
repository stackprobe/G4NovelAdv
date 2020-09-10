using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Commands
{
	public abstract class Command
	{
		public string Name;
		public string[] Arguments;

		// <---- prm

		public abstract void Loaded();
		public abstract void Fire();
	}
}
