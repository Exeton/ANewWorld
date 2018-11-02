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
				//A binary search may be more efficent
				for (int y = 0; y < map.GetLength(1); y++)
				{
					if (map[x, y].filled)
					{
						replaceAllAdjacentDirt(map, x, y - 1);
						break;
					}
				}
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
	}
}
