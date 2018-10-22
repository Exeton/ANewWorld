using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.View
{
	abstract class MapCacher
	{
		public static int regionWidthInBlocks = 100;
		public static int regionHeightInBlocks = 50;

		public abstract Bitmap getCachedRegion(int regionX, int regionY);

		public virtual void cacheRegion(int blockX, int blockY, Bitmap regionBlocks)
		{
			cacheRegion(blockX / regionWidthInBlocks, blockY / regionHeightInBlocks, regionBlocks);
		}
		public abstract void chacheRegion(int regionX, int regionY, Bitmap regionBlocks);
	}
}
