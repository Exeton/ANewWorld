using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	[Serializable]
	public class Block
	{
		public static int blockSize = 10;

		[NonSerialized]
		private Brush brushValue;
		public Brush brush
		{
			get
			{
				return brushValue;
			}
		}

		public bool filled;

		private Color colorValue;
		public Color color
		{
			get
			{
				return colorValue;
			}
			set
			{
				colorValue = value;
				brushValue = new SolidBrush(color);
			}
		}

		public void onDeseralized()
		{
			color = color;
		}

		public Block(bool filled, Color color)
		{
			this.filled = filled;
			this.color = color;
			this.brushValue = new SolidBrush(color);
		}
	}
}
