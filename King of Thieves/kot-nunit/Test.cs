using NUnit.Framework;
using System;
using King_of_Thieves.MathExt;
using Microsoft.Xna.Framework;

namespace kotnunit
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			Assert.AreEqual(MathExt.distance(new Vector2(0, 0), new Vector2(0, 1)), 1.0f);
		}
	}
}

