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
		public void generate(Map map, int regionStartX, int regionEndX)
		{
			for (int x = regionStartX; x < regionEndX; x++)
			{
				//A binary search may be more efficent
				for (int y = 0; y < map.GetLength(1); y++)
				{
					if (map[x, y].filled)
					{
						map[x, y].type = (int)BlockType.GRASS;
						map[x, y + 1].type = (int)BlockType.GRASS;
						break;
					}
				}
			}
		}
	}
}
