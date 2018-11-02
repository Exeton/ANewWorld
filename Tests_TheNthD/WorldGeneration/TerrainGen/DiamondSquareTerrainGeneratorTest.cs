using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D;
using TheNthD.WorldGeneration.TerrainGen;

namespace Tests_TheNthD
{
	[TestClass]
	public class DiamondSquareTerrainGeneratorTest
	{
		[TestMethod]
		public void testTerrainHeightMapMatch()
		{
			Map map = new Map(256, 200, "testMap");

			int seed = 123123123;
			Random random1 = new Random(seed);
			DiamondSquareTerrainGenerator terrainGenerator = new DiamondSquareTerrainGenerator(5, random1);
			terrainGenerator.generate(map, 0, 256);


			Random random2 = new Random(seed);
			terrainGenerator = new DiamondSquareTerrainGenerator(5, random2);
			int[] sameHeightMap = terrainGenerator.generateHeightMap(257, random2.Next(50, 100), random2.Next(50, 100));

			bool sameHeights = true;
			for (int i = 0; i < map.GetLength(0); i++)
			{
				int height = sameHeightMap[i];
		
				if (!map[i, height].filled)
					sameHeights = false;

				if (map[i, height - 1].filled)
					sameHeights = false;

			}

			Assert.IsTrue(sameHeights);
		}
	}
}
