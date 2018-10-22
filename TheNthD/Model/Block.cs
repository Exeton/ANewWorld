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
		public int type;

		public Block(bool filled, int type)
		{
			this.filled = filled;
			this.type = type;
		}
	}
}
