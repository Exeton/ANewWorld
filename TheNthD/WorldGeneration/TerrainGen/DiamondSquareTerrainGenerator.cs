using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D;
using The_Nth_D.Model;

namespace TheNthD.WorldGeneration.TerrainGen
{
	public class DiamondSquareTerrainGenerator : ITerrainGenerationTask
	{
		Random r;
		int roughness;

		public DiamondSquareTerrainGenerator(int roughness, Random random)
		{
			this.roughness = roughness * 5;
			r = random;
		}

		public void generate(Map map, int regionStartX, int regionEndX)
		{
			if (!checkGenerationRegionValidity(regionStartX, regionEndX))
			{
				throw new Exception("Invalid region size. Diamond square regions must be 2^n + 1 size");
			}

			int[] heightMap = generateHeightMap(regionEndX - regionStartX + 1, r.Next(50, 100), r.Next(50, 100));
			generateTerrain(map, heightMap, regionStartX);
		}

		private bool checkGenerationRegionValidity(int left, int right)
		{
			int width = 1 + right - left;

			//width must be 2^n + 1
			double widthLog = Math.Log(width - 1, 2);

			if (widthLog == (int)widthLog)
				return true;
			return false;
		}

		public int[] generateHeightMap(int width, int leftHeight, int rightHeight)
		{
			int[] heights = new int[width];
			heights[0] = leftHeight;
			heights[width - 1] = rightHeight;
			int lastHeightArrayIndex = width - 1;

			int iteration = 0;
			double heightIndexesToCalculate = 1;
			bool generating = true;
			while (generating)
			{
				//SegmentSize is out of 1, not the map size
				double segmentSize = (double)(1 / (2d * heightIndexesToCalculate));
				double priorSegmentSize = (double)(1d / heightIndexesToCalculate);

				int randomRange = (int)(roughness / Math.Pow(iteration + 1, 2));

				for (double i = segmentSize; i < 1; i += priorSegmentSize)
				{
					int index = (int)(width * i);
					int leftHeightIndex = (int)(lastHeightArrayIndex * (i - segmentSize));
					int rightHeightIndex = (int)(lastHeightArrayIndex * (i + segmentSize));

					int avgHeight = (heights[leftHeightIndex] + heights[rightHeightIndex]) / 2;
					int newHeight = r.Next(avgHeight - randomRange, avgHeight + randomRange);

					heights[index] = newHeight;
				}

				iteration++;
				heightIndexesToCalculate *= 2;

				if (segmentSize * (width - 1) <= 1)
					generating = false;
			}

			return heights;
		}

		private void generateTerrain(Map map, int[] heightMap, int regionStartX)
		{
			for (int i = 0; i < heightMap.Length; i++)
			{
				int worldX = regionStartX + i;
				for (int j = heightMap[worldX]; j < map.GetLength(1); j++)
				{
					map[worldX, j] = new Block(true, 2);
				}
			}
		}
	}
}
