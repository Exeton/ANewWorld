using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D;

namespace TheNthD.WorldGeneration.TerrainGen
{
	class GrassTerrainGenerator : ITerrainGenerationTask
	{
		public void generate(Map map, int regionStartX, int regionEndInclusive)
		{
			for (int x = regionStartX; x <= regionEndInclusive; x++)
			{
				int topBlock;
				topBlock = getTopYBlock(map, x, 0);
				replaceAllAdjacentDirt(map, x, topBlock - 1);
			}
		}

		public void replaceAllAdjacentDirt(Map map, int x, int y)
		{
			for (int i = -1; i < 2; i++)
				for (int j = -1; j < 2; j++)
				{
					if (map[x + i, y + j].filled)
						map[x + i, y + j].type = (int)BlockType.GRASS;
				}
		}


		//Not garunteed to find the top block, but will do so much faster
		private int getTopYBlock(Map map, int x, int startY)
		{
			int stepSize = 5;

			int approxTopBlock = getTopYBlockHerustic(map, x, startY, stepSize);
			return getTopYBlockHerustic(map, x, approxTopBlock - stepSize * 2, 1);
		}

		private int getTopYBlockHerustic(Map map, int x, int startY, int yIncrement)
		{
			int mapLength= map.GetLength(1);
			for (int y = startY; y < mapLength; y += yIncrement)
			{
				if (map[x, y].filled)
					return y;
			}
			return -1;
		}
	}
}
