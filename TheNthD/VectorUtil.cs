using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNthD
{
	public class VectorUtil
	{
		public static Vector2 positivePerpindicularVector(Vector2 vector2)
		{
			return new Vector2(Math.Abs(vector2.Y), Math.Abs(vector2.X));
		}

		public static Vector2 velocityAndDimensionToVector(float velocity, int dimension, float val)
		{
			if (velocity < 0)
				val *= -1;

			if (dimension == 0)
				return new Vector2(val, 0);
			if (dimension == 1)
				return new Vector2(0, val);

			throw new Exception("Invalid dimension");
		}

		public static Vector2 velocityAndDimensionToUnitVector(int velocity, int dimension)
		{
			return velocityAndDimensionToVector(velocity, dimension, 1);
		}

		public static Vector2 getUnitUp()
		{
			return new Vector2(0, -1);
		}
		public static Vector2 getUnitDown()
		{
			return new Vector2(0, 1);
		}
		public static Vector2 getUnitLeft()
		{
			return new Vector2(-1, 0);
		}
		public static Vector2 getUnitRight()
		{
			return new Vector2(1, 0);
		}
	}
}
