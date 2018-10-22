using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	[Serializable]
	public class Block
	{
		public static int blockSize = 10;
		public bool filled;
		public Color color;

		public Block(bool filled, Color color)
		{
			this.filled = filled;
			this.color = color;
		}
	}
}
