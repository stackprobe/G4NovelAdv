using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Logos;

namespace Charlotte.Tests.Logos
{
	public class LogoTest
	{
		public void Test01()
		{
			using (new Logo())
			{
				Logo.I.Perform();
			}
		}
	}
}
