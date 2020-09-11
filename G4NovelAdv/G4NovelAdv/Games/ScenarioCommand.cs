﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Surfaces;

namespace Charlotte.Games
{
	public class ScenarioCommand
	{
		private string[] Tokens;

		public ScenarioCommand(string[] tokens)
		{
			this.Tokens = tokens;
		}

		public void Invoke()
		{
			if (this.Tokens[1] == "=")
			{
				string instanceName = this.Tokens[0];
				string typeName = this.Tokens[2];

				Surface surface = SurfaceCreator.Create(typeName, instanceName);

				Game.I.Status.Surfaces.Add(surface);
			}
			else
			{
				string instanceName = this.Tokens[0];
				string command = this.Tokens[1];
				string[] arguments = this.Tokens.Skip(2).ToArray();

				Surface surface = Game.I.Status.GetSurface(instanceName);

				surface.Invoke(command, arguments);
			}
		}
	}
}
