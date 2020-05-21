using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Scenarios.Resources;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public ResourceMusic Music = new ResourceMusic();
		public ResourcePicture Picture = new ResourcePicture();
		public ResourceSE SE = new ResourceSE();

		public int MessageSpeed = Consts.MESSAGE_SPEED_DEF;

		public ScenarioResPicture ScenarioResPicture = new ScenarioResPicture();
	}
}
