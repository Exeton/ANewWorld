using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using The_Nth_D;
using TheNthD;

namespace Tests_TheNthD
{
	[TestClass]
	public class VectorUtilTest
	{
		[TestMethod]
		public void positivePerpindicularVectorTest()
		{
			Vector2 expected = new Vector2(7, 5);
			Vector2 result = VectorUtil.positivePerpindicularVector(new Vector2(-5, 7));

			Assert.IsTrue(areEqual(expected, result));		
		}

		[TestMethod]
		public void velocityAndDimensionToVectorTest()
		{
			Vector2 expected = new Vector2(7, 0);
			Vector2 result = VectorUtil.velocityAndDimensionToVector(1, 0, 7);

			Assert.IsTrue(areEqual(expected, result));
		}

		[TestMethod]
		public void velocityAndDimensionToVectorTest2()
		{
			Vector2 expected = new Vector2(-7, 0);
			Vector2 result = VectorUtil.velocityAndDimensionToVector(-1, 0, 7);

			Assert.IsTrue(areEqual(expected, result));
		}

		[TestMethod]
		public void velocityAndDimensionToVectorTest3()
		{
			Vector2 expected = new Vector2(0, 8);
			Vector2 result = VectorUtil.velocityAndDimensionToVector(-1, 1, -8);

			Assert.IsTrue(areEqual(expected, result));
		}

		[TestMethod]
		public void velocityAndDimensionToUnitVectorTest()
		{
			Vector2 expected = new Vector2(0, -1);
			Vector2 result = VectorUtil.velocityAndDimensionToUnitVector(-20, 1);

			Assert.IsTrue(areEqual(expected, result));
		}


		private static bool areEqual(Vector2 vectorA, Vector2 vectorB)
		{
			return (vectorA.X == vectorB.X) && (vectorA.Y == vectorB.Y);
		}
	}
}
